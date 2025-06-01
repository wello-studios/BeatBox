using System;
using BeatBox.Enum;
using BeatBox.System;
using BeatBox.System.Manager;
using BeatBox.System.ObjectPooling;
using BeatBox.Util;
using UnityEngine;

/* Note
 * 노트.
 *
 * [VARIABLE]
 * Line line
 *   현재 Note가 속한 Line.
 * NoteType noteType
 *   현재 Note의 종류.
 * double judgeTick
 *   현재 Note가 처리될 예정인 Tick
 * bool isCircle
 *   노트가 원형인가?
 * bool isMetr
 *   메트로놈(단위마디마다 나타나는 불투명한 원)
 * 
 * [METHOD]
 * void INIT
 *   초기화.
 *   노트의 여러 정보들을 종합적으로 검토함.
 * void Move
 *   노트를 노트 속도 배율, 노트 전체 속도, 게임 속도의 곱의 속도로 이동시킴.
 * void OnCollideJudgeLine
 *   노트와 판정선이 만났을때
 * void CheckItNeedsToDelete
 *   이 노트가 뒤@져야 하는지 여부 판단
 * void DeleteNote
 *   노트 창@자 빼기
 *
 * [UNITY EVENT]
 * AWAKE -
 * START -
 * UPDATE - GM의 isPaused가 false가 아닐때
            초기화 하지 않았으면 INIT 호출
            이후 아래 메서드들을 호출
            Move
            CheckItNeedsToDelete
            OnCollideJudgeLine (isOverJL이 false라면)
 */

namespace BeatBox.Note
{
    public class Note : MonoBehaviour
    {
        public Line line;
        public NoteType noteType;
        
        public float speedMultipler;
        public double judgeTick;
        
        private bool isOverJL = false;
        
        public bool isCircle;
        public bool isMetr;
        public CircleRenderer circleRenderer;

        private float _lastPosX;
        
        public GameManager gameManager;
        public TickManager tickManager;
        public ObjectPoolingObject objectPoolingObject;
        
        private bool _inited = false;

        public static void OnJudged()
        {
            // TODO
        }
        
        void Init()
        {
            if (isCircle) circleRenderer.lineRenderer.enabled = false;

            gameManager = GameManager.instance;
            tickManager = TickManager.instance;

            if (!gameManager || !tickManager) return;
            
            circleRenderer = (isCircle) ? GetComponent<CircleRenderer>() : null;

            if (!isCircle)
            {
                transform.localPosition = new Vector3((float)gameManager.noteSpawnPos, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                circleRenderer.radius = (float)gameManager.noteSpawnPos;
            }

            line = transform.GetComponentInParent<Line>();
            if (isCircle) circleRenderer.lineRenderer.enabled = true;
            
            _inited = true;
        }

        void Move()
        {
            if (!isCircle)
            {
                transform.localPosition = new Vector3(
                    (float)(transform.localPosition.x
                            - Time.deltaTime * speedMultipler
                                             * gameManager.GameSpeed.GetValue(tickManager.absoluteTick)
                                             * gameManager.NoteSpeed.GetValue(tickManager.tick)
                                             * Math.Pow(PlayerCustomData.NoteSpeedMult, 2)),
                    transform.localPosition.y,
                    transform.localPosition.z);
            }
            else
            {
                circleRenderer.radius -= (float)
                    (Time.deltaTime * speedMultipler
                                    * gameManager.GameSpeed.GetValue(tickManager.absoluteTick)
                                    * gameManager.NoteSpeed.GetValue(tickManager.tick)
                                    * Math.Pow(PlayerCustomData.NoteSpeedMult, 2));
            }
        }

        
        public void OnCollideJudgeLine()
        {
            isOverJL = true;
            
            if (noteType == NoteType.Metronome) {DeleteNote();}
        }

        public void CheckItNeedsToDelete()
        {
            if (!isCircle)
            {
                if (transform.localPosition.x <= 0)
                {
                    DeleteNote();
                }
            }
            else
            {
                if (circleRenderer.radius <= 0)
                {
                    DeleteNote();
                }
            }
        }

        public void DeleteNote()
        {
            gameObject.SetActive(false);
            objectPoolingObject.parent.TakeIn(gameObject);
        }
        
        void OnEnable()
        { 
            Init();
        }

        void Update()
        {
            if (gameManager.isPaused) return;
            if (!_inited) Init();

            Move();
            CheckItNeedsToDelete();

            if (!isOverJL) OnCollideJudgeLine();
        }
    }
}