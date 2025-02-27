using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BeatBox.Cube
{
    public class CubeInputManager : MonoBehaviour
    {
        [Header("InputActions")]
        public InputAction keyW;
        public InputAction keyA;
        public InputAction keyS;
        public InputAction keyD;
        public InputAction keyLarrow;
        public InputAction keyRarrow;
        public InputAction keyEnter; 
        public InputAction keySpace;
        public InputAction keyEscape;

        [Header("KeyPressHold")]
        public bool pressHoldKeyW;
        public bool pressHoldKeyA;
        public bool pressHoldKeyS;
        public bool pressHoldKeyD;
        public bool pressHoldKeyL;
        public bool pressHoldKeyR;
        public bool pressHoldKeySpace;
        public bool pressHoldKeyEscape;
        public bool pressHoldKeyEnter;

        [Header("KeyDown")]
        public bool pressDownKeyW;
        public bool pressDownKeyA;
        public bool pressDownKeyS;
        public bool pressDownKeyD;
        public bool pressDownKeyL;
        public bool pressDownKeyR;
        public bool pressDownKeySpace;
        public bool pressDownKeyEscape;
        public bool pressDownKeyEnter;
        
        [Header("KeyDown")]
        public bool pressUpKeyW;
        public bool pressUpKeyA;
        public bool pressUpKeyS;
        public bool pressUpKeyD;
        public bool pressUpKeyL;
        public bool pressUpKeyR;
        public bool pressUpKeySpace;
        public bool pressUpKeyEscape;
        public bool pressUpKeyEnter;
        
        public bool tempW      = false;
        public bool tempA      = false;
        public bool tempS      = false;
        public bool tempD      = false;
        public bool tempL      = false;
        public bool tempR      = false;
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
            
            pressHoldKeyL = keyLarrow.inProgress;
            pressDownKeyL = keyLarrow.triggered;
            pressUpKeyL = (tempL && !pressHoldKeyL);
            tempL = pressHoldKeyL;
            
            pressHoldKeyR = keyRarrow.inProgress;
            pressDownKeyR = keyRarrow.triggered;
            pressUpKeyR = (tempR && !pressHoldKeyR);
            tempR = pressHoldKeyR;
            
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
            keyEnter.Disable();
            keySpace.Disable();
            keyEscape.Disable();
        }

        private void Update()
        {
            GetInput();
        }
    }
}
