
using UnityEngine;

/* CubeVisualManager
 * 큐브의 입력에 따른 화살표 표시
 *
 * [VARIABLE]
 * GameObject FArrow
 *   F방향의 화살표 GameObject.
 *
 * [METHOD]
 * SetArrows
 *   화살표 오브젝트의 크기를 입력에 따라 조정한다.
 *
 * [UNITY EVENT]
 * AWAKE - CIM을 불러온다.
 * START -
 * UPDATE - SetArrows 호출
 */

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

        void SetArrows()
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
            SetArrows();
        }
    }
}
