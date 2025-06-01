using System.Collections.Generic;
using BeatBox.Cube;
using BeatBox.Enum;
using BeatBox.Note;
using BeatBox.System.Manager;
using BeatBox.System.ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

/* LINE
 * a line that note comes.
 *
 * [about GuideLine]
 *   이 라인의 입력 여부 확인용 오브젝트.
 * 
 * [VARIABLE]
 * List<double> lineNoteTTicks
 *   처리방법이 T인 노트의 틱
 * List<double> lineNoteTTytpes
 *   처리방법이 T인 노트의 종류
 * List<Note> lineNoteTObjects
 *   처리방법이 T인 노트의 스크립트
 * GameObject inputGuideObject
 * 
 * [METHOD]
 * Note SpawnNote (int noteType)
 *   noteType에 맞는 Note를 이 Line에 생성한다. ( r=40 )
 *   return (생성한 Note의 스크립트)
 * void CheckNoteTicks
 *   이 Line의 판정할 Note가 Cube를 지나쳐 Miss가 나오는지 확인
 * void Judge
 *   이 Line의 가장 앞 처리방법에 맞는 Note를 Judge함.
 * void InputGuideObjectActive
 *   이 Line의 입력 여부를 확인하고 GuideLine의 표시 여부를 제어함.
 * 
 * [UNITY EVENT]
 * AWAKE - 
 * START - Line을 초기화함.
 * UPDATE - GM의 isPaused이 false라면 아래 메서드를 호출함.
            InputGuideObjectActive
            CheckNoteTicks
            Judge
 */

namespace BeatBox.Note
{
    public class Line : MonoBehaviour
    {
        public int lineNumber;

        public double judgeMult;
        
        public List<double> lineNoteDownTicks;
        public List<double> lineNoteDownTypes;
        public List<Note>   lineNoteDownObjects;
        
        public List<double> lineNoteUpTicks;
        public List<double> lineNoteUpTypes;
        public List<Note>   lineNoteUpObjects;
        
        public List<double> lineNoteSLTicks;
        public List<Note>   lineNoteSLObjects;
        
        public List<double> lineNoteSRTicks;
        public List<Note>   lineNoteSRObjects;
        
        public ObjectPoolingManager[] objectPoolingManagers;

        public CubeJudgeManager cubeJudgeManager;
        public CubeInputManager cubeInputManager;
        
        public TickManager tickManager;
        public GameManager gameManager;
        public NoteManager noteManager;

        public GameObject inputGuideObject;

        public GameObject SpawnNote(NoteType noteType)
        {
            return objectPoolingManagers[(int)noteType].TakeOut();
        }

        public Note SpawnNote(int noteType)
        {
            //Debug.Log("[LINE"+lineNumber+"]"+"noteType = "+noteType);
            var note = objectPoolingManagers[noteType].TakeOut().GetComponent<Note>();
            if (JudgeManager.GetInputType((NoteType)noteType) == BBInputType.Down   ) {lineNoteDownObjects.Add(note);}
            if (JudgeManager.GetInputType((NoteType)noteType) == BBInputType.Up     ) {lineNoteUpObjects  .Add(note);}
            if (JudgeManager.GetInputType((NoteType)noteType) == BBInputType.RotateL) {lineNoteSLObjects  .Add(note);}
            if (JudgeManager.GetInputType((NoteType)noteType) == BBInputType.RotateR) {lineNoteSRObjects  .Add(note);}
            
            return note;
        }

        public void CheckNoteTicks()
        {
            while (true)
            {
                if (lineNoteDownTicks.Count == 0) break;
                
                if (JudgeManager.GetJudgeType(lineNoteDownTicks[0], judgeMult) == JudgeType.None
                    && tickManager.tick > lineNoteDownTicks[0])
                {
                    Debug.Log("["+lineNumber+"] MISS!");
                    JudgeManager.ApplyJudge(JudgeType.D0);
                    lineNoteDownTicks.RemoveAt(0);
                }
                else break;
            }
        }

        public void Judge()
        {
            if (lineNumber == 9) return;
            if (lineNumber == 8)
            {
                if (cubeInputManager.pressDownKeyLeft)
                {
                    //Debug.Log("Rotate to Left");
                }
                if (cubeInputManager.pressDownKeyRight)
                {
                    //Debug.Log("Rotate to Right");
                }
            }
            
            if (cubeJudgeManager.lineInputDown[lineNumber])
            {
                //return;
                
                // DOWN
                
                // TODO
                /* 1.   lineNoteDownTicks의 첫 요소를 꺼낸다 -> (var)target
                 * 1-1. JudgeManager::Judge()로 판정 구하기
                 * 2.   노트 오브젝트 구하기
                 * 3.   JudgeManager::ApplyJudge()로 적용하기
                 */

                if (lineNoteDownTicks.Count == 0) return;
                
                // [1][2]
                var targetTick = lineNoteDownTicks[0];
                
                // [1] - 1
                JudgeType jt = JudgeManager.GetJudgeType(targetTick, judgeMult);

                if (jt == JudgeType.None) { return; }
                
                
                // [3]
                JudgeManager.ApplyJudge(jt);
                
                                
                if (lineNoteDownObjects.Count == 0)
                { // happend on First noteTick and Last noteTick
                    if (targetTick < tickManager.tick)
                    { // FIRST
                        noteManager.noteMapSTArray[lineNumber].RemoveAt(0);
                        noteManager.noteMapTTClone[lineNumber].RemoveAt(0);
                    }
                }
                else
                {
                    var noteObject = lineNoteDownObjects[0];
                    if (noteObject.judgeTick == targetTick)
                    {
                        noteObject.DeleteNote();
                        lineNoteDownObjects.RemoveAt(0);
                    }
                }
                
                lineNoteDownTicks.RemoveAt(0);
            }
            
            if (cubeJudgeManager.lineInputUp[lineNumber])
            {
                // UP  
            }
        }

        public void InputGuideObjectActive()
        {
            if (!inputGuideObject) return;
            inputGuideObject.SetActive(cubeJudgeManager.lineInputHold[lineNumber]);
        }

        void OnEnable()
        {
            transform.rotation = Quaternion.Euler(0, 0, 90 + 45 * lineNumber);
        }

        void Start()
        {
            tickManager = TickManager.instance;
            gameManager = GameManager.instance;
            noteManager = NoteManager.instance;

            cubeJudgeManager = Cube.Cube.judgeManager;
            cubeInputManager = Cube.Cube.inputManager;
        }

        void Update()
        {
            if (gameManager.isPaused) return;
            
            InputGuideObjectActive();
            CheckNoteTicks();
            Judge();
        }
    }
}