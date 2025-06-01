using System;
using UnityEngine;
using Unity.Mathematics;
using BeatBox.Cube;

/* CubeRotateManager
 * 큐브가 돌아간 정도를 계산하고, 큐브를 회전시킴.
 * 
 * [about rotate]
 * | 1 0 7 |
 * | 2 C 6 |
 * | 3 4 5 |
 * (id of rotate and C=Cube)
 * 
 * [VARIABLE]
 * int rotateNumber
 * 
 * float rotateSpeed
 *
 * float _velocity
 *   Mathf.SmoothDampAngle을 사용하므로.
 * 
 * [METHOD]
 * void SetRotateNumber
 *   입력에 따라 회전 정도를 바꾼다.
 * 
 * [UNITY EVENT]
 * AWAKE - CIM, CRM을 불러온다.
 * START -
 * UPDATE - SetRotateNumbers, SetCubeRotateValue을 호출한다.
 */

namespace BeatBox.Cube
{
    public class CubeRotateManager : MonoBehaviour
    {
        
        /*[ VARIABLE ]*/
        public int rotateNumber;
        public float rotateSpeed;

        private float _velocity;
        
        private CubeInputManager  _cim;
        private CubeRotateManager _crm;
        
        /*[ METHOD ]*/
        private void SetRotateNumbers()
        {
            if (_cim.pressDownKeyLeft) { rotateNumber = Cube.NormalizeLineNumber(rotateNumber+1, false); };
            if (_cim.pressDownKeyRight) { rotateNumber = Cube.NormalizeLineNumber(rotateNumber-1, false); };
        }
        
        private void SetCubeRotateValue()
        {
            var absoluteRotate = (rotateNumber) * 45;
            var lerpRotate = Mathf.SmoothDampAngle(transform.eulerAngles.z, absoluteRotate, ref _velocity, rotateSpeed*Time.deltaTime);
        
            transform.localRotation = Quaternion.Euler(0, 0, lerpRotate);
        }
        
        /*[ UNITY EVENT ]*/
        private void Awake()
        {
            _cim = GetComponent<CubeInputManager >();
            _crm = GetComponent<CubeRotateManager>();
        }
        
        private void Update()
        {
            SetRotateNumbers();
            SetCubeRotateValue();
        }
    }
}
