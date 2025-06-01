using UnityEngine;

/* { TickManager }
 *
 * [VARIABLE]
 * float absoluteTick
 *   절대틱 ( GameSpeed에 영향받지 않음. )
 * float tick
 *   틱 ( GameSpeed에 영향받음. )
 *
 * [METHOD]
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH.
 * START - GM을 가져옴.
 * UPDATE - 틱 계산.
 */

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