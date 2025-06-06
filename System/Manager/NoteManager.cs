using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Linq;
using BeatBox.Enum;
using BeatBox.Note;

// IF U SEE THIS MESSAGE,
// PLZ RUN AWAY FROM HERE
// THIS PLACE IS HELL

/* { NoteManager }
 *
 * [VARIABLE]
 * List<double[]>[] noteMapTTArray = new List<double[]>[10];
 * List<double[]>[] noteMapTTClone = new List<double[]>[10];
 * List<double[]>[] noteMapSTArray = new List<double[]>[10];
 *   TT        : 곡의 노트 정보 (노트가 판정선에 도착한 틱)
                 Judge 과정에서 다소 변경됨.
 *   TT(Clone) : TT의 원본. 변경되지 않음.
 *   ST        : 곡의 노트 정보 (노트가 생성되는 틱)
 * 
 * [METHOD]
 * void INIT (int songLength)
 *   초기화 작업
 *
 * void SpawnNoteObject
 *   곡 노트 정보에 따라 노트 소환
 *
 * double GetOffset(double targetTick, float noteSpeed)
 *   targetTick에 노트가 판정선에 있을 수 있도록 하는 생성 Tick을 반환하는 함수.
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH., 초기화 작업 준비.
 * START - 
 * UPDATE - 초기화 작업을 진행했을때, SpawnNoteObject 호출.
 */

namespace BeatBox.System.Manager
{
    public class NoteManager : MonoBehaviour
    {
        public List<Line> lines;
        public GameObject lineObject;

        public GameObject[] noteObjects;
        
        public GameManager _gameManager;
        public TickManager _tickManager;
        public CameraManager _cameraManager;

        public int noteCount;
        
        private bool _inited;

        [SerializeField] private int _currMetronomeTick = 0;
        [SerializeField] private double _currMetronomeSTick = 0;
        
        // [T, NoteType, NoteSpeedMult.]
        public List<double[]>[] noteMapTTArray = new List<double[]>[10];
        public List<double[]>[] noteMapTTClone = new List<double[]>[10];
        public List<double[]>[] noteMapSTArray = new List<double[]>[10];

        public static NoteManager instance;

        public void INIT(int songLength)
        {
            // set components
            _gameManager = GameManager.instance;
            _tickManager = TickManager.instance;
            _cameraManager = CameraManager.instance;

            noteMapTTClone = noteMapTTArray;
            
            // count all notes
            noteCount = 0;
            for (var k = 0; k < 8; k++)
            {
                noteCount += noteMapTTArray[k].Count;
            }
            
            // set score for each judge
            var progressManager = Progressmanager.instance;
            progressManager.scoreForJudge[0] =           ((float)progressManager.maxScore / noteCount);
            progressManager.scoreForJudge[1] = (4f/5f) * ((float)progressManager.maxScore / noteCount);
            progressManager.scoreForJudge[2] = (3f/5f) * ((float)progressManager.maxScore / noteCount);
            progressManager.scoreForJudge[3] = (2f/5f) * ((float)progressManager.maxScore / noteCount);
            progressManager.scoreForJudge[4] = (1f/5f) * ((float)progressManager.maxScore / noteCount);
            progressManager.scoreForJudge[5] = 0;
                
            // add Metronome
            for (var j = 2; j < Math.Ceiling(songLength* (_tickManager.BPM / 60f) / 4f); j++)
            {
                noteMapTTArray[9].Add(new [] { (j * 4) , 2d, 1d});
            }
            
            // init : lines
            var i = 0;
            foreach (var noteMapTT in noteMapTTArray)
            {
                lines[i].lineNoteDownTicks = new List<double>();
                lines[i].lineNoteUpTicks   = new List<double>();
                
                // [*] Initialize line data and noteMapSTArray data on Line number 'i'
                // set : NoteManager::noteMapSTArray
                // set : Line::lineNoteDownTicks      Line::lineNoteUpTicks
                // calc : note spawn tick
                foreach (var note in noteMapTT)
                {
                    if (i == 8 && note[1] == 0) { return; }
                    
                    // [1][2]
                    if (JudgeManager.GetInputType((NoteType)((int)note[1])) == BBInputType.Down) lines[i].lineNoteDownTicks.Add(note[0]);
                    if (JudgeManager.GetInputType((NoteType)((int)note[1])) == BBInputType.Up  ) lines[i].lineNoteUpTicks  .Add(note[0]);
                    
                    // [-] lines[i].lineNoteTicks[(int)note[1]].Add(note[0]);
                    
                    // [3]
                    var offset = note[0] - GetOffset(note[0], (float)note[2]);
                    noteMapSTArray[i].Add(new [] { note[0] - offset * _tickManager.BPM / 60f , note[1], note[2] });
                }

                // [*] Finish step
                // set : Line::lineNoteDownTicks
                //       Line::lineNoteUpTicks
                lines[i].lineNoteDownTicks.Sort();
                lines[i].lineNoteUpTicks.Sort();
                
                i++;
            }

            _inited = true;
        }
        
        public void SpawnNoteObject()
        {
            if (!_inited) return;
            
            // [*] for check to each line
            for (var i=0; i<10; i++) // i = line number
            {
                // get : NoteManager::noteMapSTArray
                var noteMapST = noteMapSTArray[i];
                
                // [*] when note on line 0
                if (noteMapST.Count == 0) { continue; }
                
                // [*] spawn all note that need to spawn
                // set : (var)note
                //       note::speedMultipler note::noteData
                if (noteMapST[0][0] <= _tickManager.tick)
                {
                    foreach (var line in lines)
                    {
                        if (line.lineNumber == i)
                        {
                            var note = line.SpawnNote((int)noteMapST[0][1]);
                            note.speedMultipler = (int)noteMapST[0][2];
                            note.noteType = (NoteType)((int)noteMapST[0][1]);
                            
                            // if (i!=9) Debug.Log(noteSpawnedTimes[i]+": noteSpawnedTimes | "+noteMapTTArray[i][noteSpawnedTimes[i]][0]+": judgeTick");
                            if (i!=9) note.judgeTick = noteMapTTClone[i][0][0];
                            break;
                        }
                    }
                    noteMapST.RemoveAt(0);
                    noteMapTTClone[i].RemoveAt(0);
                }
            }
        }

        public double GetOffset(double targetTick, float noteSpeed)
        {
            var tickLists = new List<double>() { };
            tickLists.AddRange(_gameManager.NoteSpeed.Ticks);
            tickLists.Add(targetTick);
            tickLists = tickLists.Distinct().ToList();
            tickLists.Sort();
            tickLists.Reverse();

            var targetDistance = _gameManager.noteSpawnPos;

            var i = -1;
            
            double result = 0;
            
            /* i bet this will not work well in 1st try. - 01/09 11:38 */
            /* because idk what i've written                           */
            
            // With bunch of try, this func working very well.
            
            foreach (var tick in tickLists)
            {
                i++;

                if (i == tickLists.Count-1) return result;
                
                var nTick = tickLists[i + 1];
                
                // 1. 목표 틱보다 큰 틱은 배제
                if (tick > targetTick)
                {
                    continue;
                }
                
                var c2 = _gameManager.NoteSpeed.GetCurveWithTick(nTick);
                
                if (c2.isConstant)
                {
                    if (c2.startValue == 0)
                    {
                        targetTick -= (tick - nTick);
                        continue;
                    }
                    else
                    {
                        var b = c2.startValue;
                        var s = noteSpeed * Math.Pow(PlayerCustomData.NoteSpeedMult, 2);
                        var d = targetTick;

                        var r = -((targetDistance-3.2f) / (  b * s )) + d;
                        
                        if ((tick - nTick) * s * b >= targetDistance)
                        {
                            return r;
                        }
                        else
                        {
                            targetDistance -= (tick - nTick) * s * b;
                            targetTick -= (tick - nTick);
                            continue;
                        }
                    }
                }
                else
                {
                    throw new WarningException("WRONG CUBIC BEZIER CURVE");
                }
            }
            
            return -1;
        }

        public void Awake()
        {
            instance = this;

            for (int i = 0; i < 10; i++)
            {
                noteMapSTArray[i] = new List<double[]>();
            }
        }
        
        private void Update()
        {
            if (!_inited) return;

            SpawnNoteObject();
        }
    }
}