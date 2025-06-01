using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/* CUBE INPUT MANAGER
 * 인게임 모든 키입력을 관리한다.
 *
 * [VARIABLE]
 * bool isInputable
 * 
 * InputAction key~
 * 
 * bool pressHoldKey~
 * 
 * bool pressDownKey~
 * 
 * bool pressUpKey~
 * 
 * bool temp~
 *   홀드 여부 확인용
 * 
 * [METHOD]
 * void GetInput
 *   키 입력을 감지한다.
 *
 * [UNITY EVENT]
 * AWAKE -
 * START -
 * Update - isInputable이 true일때 GetInput 호출.
 */

namespace BeatBox.Cube
{
    public class CubeInputManager : MonoBehaviour
    {
        public bool isInputable = true;
        
        [Header("InputActions")]
        public InputAction keyW;
        public InputAction keyA;
        public InputAction keyS;
        public InputAction keyD;
        public InputAction keyLarrow;
        public InputAction keyRarrow;
        public InputAction keyUarrow;
        public InputAction keyDarrow;
        public InputAction keyEnter; 
        public InputAction keySpace;
        public InputAction keyEscape;

        [Header("KeyPressHold")]
        public bool pressHoldKeyW;
        public bool pressHoldKeyA;
        public bool pressHoldKeyS;
        public bool pressHoldKeyD;
        public bool pressHoldKeyLeft;
        public bool pressHoldKeyRight;
        public bool pressHoldKeyUp;
        public bool pressHoldKeyDown;
        public bool pressHoldKeySpace;
        public bool pressHoldKeyEscape;
        public bool pressHoldKeyEnter;

        [Header("KeyDown")]
        public bool pressDownKeyW;
        public bool pressDownKeyA;
        public bool pressDownKeyS;
        public bool pressDownKeyD;
        public bool pressDownKeyLeft;
        public bool pressDownKeyRight;
        public bool pressDownKeyUp;
        public bool pressDownKeyDown;
        public bool pressDownKeySpace;
        public bool pressDownKeyEscape;
        public bool pressDownKeyEnter;
        
        [Header("KeyUp")]
        public bool pressUpKeyW;
        public bool pressUpKeyA;
        public bool pressUpKeyS;
        public bool pressUpKeyD;
        public bool pressUpKeyLeft;
        public bool pressUpKeyRight;
        public bool pressUpKeyUp;
        public bool pressUpKeyDown;
        public bool pressUpKeySpace;
        public bool pressUpKeyEscape;
        public bool pressUpKeyEnter;
        
        // for get Hold
        public bool tempW      = false;
        public bool tempA      = false;
        public bool tempS      = false;
        public bool tempD      = false;
        public bool tempLeft   = false;
        public bool tempRight  = false;
        public bool tempUp     = false;
        public bool tempDown   = false;
        public bool tempSpace  = false;
        public bool tempEscape = false;
        public bool tempEnter  = false;
        
        private void GetInput()
        {
            pressHoldKeyW = keyW.inProgress;
            pressDownKeyW = keyW.triggered;
            pressUpKeyW = (tempW && !pressHoldKeyW);
            tempW = pressHoldKeyW;
            
            pressHoldKeyA = keyA.inProgress;
            pressDownKeyA = keyA.triggered;
            pressUpKeyA = (tempA && !pressHoldKeyA);
            tempA = pressHoldKeyA;
            
            pressHoldKeyS = keyS.inProgress;
            pressDownKeyS = keyS.triggered;
            pressUpKeyS = (tempS && !pressHoldKeyS);
            tempS = pressHoldKeyS;
            
            pressHoldKeyD = keyD.inProgress;
            pressDownKeyD = keyD.triggered;
            pressUpKeyD = (tempD && !pressHoldKeyD);
            tempD = pressHoldKeyD;
            
            pressHoldKeyLeft = keyLarrow.inProgress;
            pressDownKeyLeft = keyLarrow.triggered;
            pressUpKeyLeft = (tempLeft && !pressHoldKeyLeft);
            tempLeft = pressHoldKeyLeft;
            
            pressHoldKeyRight = keyRarrow.inProgress;
            pressDownKeyRight = keyRarrow.triggered;
            pressUpKeyRight = (tempRight && !pressHoldKeyRight);
            tempRight = pressHoldKeyRight;
            
            pressHoldKeyUp = keyUarrow.inProgress;
            pressDownKeyUp = keyUarrow.triggered;
            pressUpKeyUp = (tempUp && !pressHoldKeyUp);
            tempUp = pressHoldKeyUp;
            
            pressHoldKeyDown = keyDarrow.inProgress;
            pressDownKeyDown = keyDarrow.triggered;
            pressUpKeyDown = (tempDown && !pressHoldKeyDown);
            tempDown = pressHoldKeyDown;
            
            pressHoldKeySpace = keySpace.inProgress;
            pressDownKeySpace = keySpace.triggered;
            pressUpKeySpace = (tempSpace && !pressHoldKeySpace);
            tempSpace = pressHoldKeySpace;
            
            pressHoldKeyEscape = keyEscape.inProgress;
            pressDownKeyEscape = keyEscape.triggered;
            pressUpKeyEscape = (tempEscape && !pressHoldKeyEscape);
            tempEscape = pressHoldKeyEscape;
            
            pressHoldKeyEnter = keyEnter.inProgress;
            pressDownKeyEnter = keyEnter.triggered;
            pressUpKeyEnter = (tempEnter && !pressHoldKeyEnter);
            tempEnter = pressHoldKeyEnter;
        }
        private void OnEnable()
        {
            keyW.Enable();
            keyA.Enable();
            keyD.Enable();
            keyS.Enable();
            keyLarrow.Enable();
            keyRarrow.Enable();
            keyUarrow.Enable();
            keyDarrow.Enable();
            keyEnter.Enable();
            keySpace.Enable();
            keyEscape.Enable();
        }
        private void OnDisable()
        {
            keyW.Disable();
            keyA.Disable();
            keyD.Disable();
            keyS.Disable();
            keyLarrow.Disable();
            keyRarrow.Disable();
            keyUarrow.Disable();
            keyDarrow.Disable();
            keyEnter.Disable();
            keySpace.Disable();
            keyEscape.Disable();
        }

        private void Update()
        {
            if (!isInputable) return;
            
            GetInput();
        }
    }
}
