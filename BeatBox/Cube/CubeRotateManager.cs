using System;
using UnityEngine;
using Unity.Mathematics;

namespace BeatBox.Cube
{
    public class CubeRotateManager : MonoBehaviour
    {
        /**
         * 1 0 7 |
         * 2 C 6 |
         * 3 4 5 |
        */
        public int rotateNumber;
        public float rotateSpeed;

        private float _velocity;
        
        private CubeInputManager  _cim;
        private CubeRotateManager _crm;
        
        private int NormalizeLineNumber(int num)
        {
            return (int)math.fmod(num, 8);
        }

        private void SetRotateNumbers()
        {
            if (_cim.pressDownKeyLeft) { rotateNumber = NormalizeLineNumber(rotateNumber+1); };
            if (_cim.pressDownKeyRight) { rotateNumber = NormalizeLineNumber(rotateNumber-1); };
        }
        
        private void Awake()
        {
            _cim = GetComponent<CubeInputManager >();
            _crm = GetComponent<CubeRotateManager>();
            // 굴림체
        }

        private void SetCubeRotateValue()
        {
            var absoluteRotate = (rotateNumber) * 45;
            var lerpRotate = Mathf.SmoothDampAngle(transform.eulerAngles.z, absoluteRotate, ref _velocity, rotateSpeed);
        
            transform.localRotation = Quaternion.Euler(0, 0, lerpRotate);
        }

        private void Update()
        {
            SetRotateNumbers();
            SetCubeRotateValue();
        }
    }
}
