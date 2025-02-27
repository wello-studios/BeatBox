using UnityEngine;

namespace BeatBox.System.Manager
{
    public class TickManager : MonoBehaviour
    {
        private float _defaultTickRate = 1;
        public GameManager _gameManager;

        public int BPM;

        public float absoluteTick;
        public float tick;

        public static TickManager instance;
        
        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            _gameManager = GameManager.instance;
        }
        
        public void Update()
        {
            if (!GameManager.instance.isPaused)
            {
                absoluteTick +=
                    Time.deltaTime 
                    * (float)BPM / 60f;
                tick +=
                    Time.deltaTime
                    * (float)BPM / 60f
                    * (float)_gameManager.GameSpeed.GetValue(absoluteTick);
            }
        }
    }
}