
using UnityEngine;

namespace BeatBox.Cube
{
    public class CubeVisualManager : MonoBehaviour
    {
        [Header("ARROWS")]
        public GameObject upArrow;
        public GameObject downArrow;
        public GameObject leftArrow;
        public GameObject rightArrow;

        private CubeInputManager _cim;

        void Arrows()
        {
            upArrow.transform.localScale    = (_cim.pressHoldKeyW) ? Vector2.one : Vector2.one/2;
            downArrow.transform.localScale  = (_cim.pressHoldKeyS) ? Vector2.one : Vector2.one/2;
            leftArrow.transform.localScale  = (_cim.pressHoldKeyA) ? Vector2.one : Vector2.one/2;
            rightArrow.transform.localScale = (_cim.pressHoldKeyD) ? Vector2.one : Vector2.one/2;
        }
        
        private void Awake()
        {
            _cim = GetComponent<CubeInputManager>();
        }

        private void Update()
        {
            Arrows();
        }
    }
}
