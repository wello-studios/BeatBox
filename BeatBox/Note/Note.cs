using System;
using BeatBox.Enum;
using BeatBox.System;
using BeatBox.System.Manager;
using BeatBox.System.ObjectPooling;
using BeatBox.Util;
using UnityEngine;

namespace BeatBox.Note
{
    public class Note : MonoBehaviour
    {
        public Line line;
        public NoteType noteType;
        
        public float speedMultipler;
        public double judgeTick;

        public GameManager gameManager;
        public TickManager tickManager;
        public ObjectPoolingObject objectPoolingObject;

        public bool isCircle;
        public bool isMetr;
        public CircleRenderer circleRenderer;

        private float _lastPosX;

        private bool _inited = false;

        public static void OnJudged()
        {
            // TODO
        }
        
        void OnEnable()
        {
            Init();
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
                                             * Math.Pow(PlayerCustomDataManager.noteSpeedMult, 2)),
                    transform.localPosition.y,
                    transform.localPosition.z);
            }
            else
            {
                circleRenderer.radius -= (float)
                    (Time.deltaTime * speedMultipler
                                    * gameManager.GameSpeed.GetValue(tickManager.absoluteTick)
                                    * gameManager.NoteSpeed.GetValue(tickManager.tick)
                                    * Math.Pow(PlayerCustomDataManager.noteSpeedMult, 2));
            }
        }

        private bool isOverJL = false;
        public void OnCollideJudgeLine()
        {
            if (isOverJL) return;
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

        void Update()
        {
            if (gameManager.isPaused) return;
            if (!_inited) Init();

            Move();
            CheckItNeedsToDelete();

            if (isCircle)  if (circleRenderer.radius <= 4    ) OnCollideJudgeLine();
            if (!isCircle) if (transform.localPosition.x <= 4) OnCollideJudgeLine();
        }
    }
}