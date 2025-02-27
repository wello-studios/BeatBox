using System;
using System.Collections.Generic;
using BeatBox.System.Manager;
using BeatBox.Util;
using UnityEngine;

namespace BeatBox.Audio
{
    public abstract class Song : MonoBehaviour
    {
        public string songName;
        public string author;
        public Difficulty difficulty;
        public int subDiff;
        public int songLength;
        
        // to TickManager
        public int bpm;
        
        // to NoteManager, JudgeManager
        // [targetTick, noteSpeed, noteType]
        public List<double[]>[] NoteMapTTArray = new List<double[]>[]
        {
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>(),
            new List<double[]>()
        };
        
        // to GameManager
        public CubicBezierCurveGroup NoteSpeedCurve = new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());
        public CubicBezierCurveGroup GameSpeedCurve = new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());

        // to ObjectManager
        public List<GameObject> decorObjects;
        public CubicBezierCurveGroup[] DecoObjectLocationCurves = new CubicBezierCurveGroup[]
        {
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>()),
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>())
        };
        public CubicBezierCurveGroup   DecoObjectRotationCurves = new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());
        public CubicBezierCurveGroup[] DecoObjectScaleCurves    = new CubicBezierCurveGroup[]
        {
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>()),
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>())
        };

        // to CameraManager
        public Sprite backgroundSprite;
        public CubicBezierCurveGroup[] CamLocationCurves = new CubicBezierCurveGroup[]
        {
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>()),
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>())
        };
        public CubicBezierCurveGroup   CamRotationCurves = new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());
        public CubicBezierCurveGroup   CamScaleCurves    = new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());

        private GameManager   _gameManager  ;
        private NoteManager   _noteManager  ;
        private TickManager   _tickManager  ;
        private AudioManager  _audioManager ;
        private CameraManager _cameraManager;
        private JudgeManager  _judgeManager ;

        private void GetManagers()
        {
            _gameManager   = GameManager  .instance;
            _noteManager   = NoteManager  .instance;
            _tickManager   = TickManager  .instance;
            _cameraManager = CameraManager.instance;
            _audioManager  = AudioManager .instance;
        }

        private void SetUp()
        {
            _gameManager.NoteSpeed = NoteSpeedCurve;
            _gameManager.GameSpeed = GameSpeedCurve;
            
            _gameManager.songName = songName;
            _gameManager.author = author;
            _gameManager.difficulty = difficulty;

            _noteManager.noteMapTTArray = NoteMapTTArray;

            _tickManager.BPM = bpm;

            _cameraManager._locationCurve = CamLocationCurves;
            _cameraManager._rotationCurve = CamRotationCurves;
            _cameraManager._scaleCurve    = CamScaleCurves;
        }

        private void Finish()
        {
            _noteManager.INIT(songLength);
            _cameraManager.inited = true;
            _audioManager.inited = true;
            _gameManager.Resume();
            
            _gameManager.GetComponent<PreLoadManager>().FinishInitialization();
            _noteManager.GetComponent<PreLoadManager>().FinishInitialization();
            _tickManager.GetComponent<PreLoadManager>().FinishInitialization();
            _judgeManager.GetComponent<PreLoadManager>().FinishInitialization();
            _audioManager.GetComponent<PreLoadManager>().FinishInitialization();
            _cameraManager.GetComponent<PreLoadManager>().FinishInitialization();
        }
        
        private void Start()
        {
            GetManagers();
            SetUp();
            Finish();
        }
    }
}