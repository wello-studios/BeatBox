using System.Collections.Generic;
using BeatBox.Util;
using UnityEngine;

/* { Cameramanager }
 *
 * [VARIABLE]
 * CubicBezierCurveGroup[2] _locationCurve
 * CubicBezierCurveGroup _scaleCurve
 * CubicBezierCurveGroup _rotationCurve
 *   카메라의 틱에 대한 위치/크기/회전 값의 그래프
 *
 * [METHOD]
 * void ModifyCamera
 *   카메라의 위치/크기/회전의 값을 그래프의 값으로 변경
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH.
 * START - TM과 Camera를 불러온다.
 * UPDATE - 초기화되었을때 ModifyCamera 호출
 */

namespace BeatBox.System.Manager
{
    public class CameraManager : MonoBehaviour
    {
        public CubicBezierCurveGroup[] _locationCurve = new[]
        {
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>()),
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>())
        };

        public CubicBezierCurveGroup _scaleCurve =
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());

        public CubicBezierCurveGroup _rotationCurve =
            new CubicBezierCurveGroup(new List<double>(), new List<CubicBezierCurve>());

        public TickManager tickManager;
        public new Camera camera;
        public static CameraManager instance;

        public bool inited = false;

        private void ModifyCamera()
        {
            transform.position = new Vector3(
                (float)_locationCurve[0].GetValue(tickManager.tick),
                (float)_locationCurve[1].GetValue(tickManager.tick),
                -10
            );
            transform.rotation = Quaternion.Euler(
                0,
                0,
                (float)_rotationCurve.GetValue(tickManager.tick)
            );
            transform.localScale = new Vector3(
                (float)_scaleCurve.GetValue(tickManager.tick),
                (float)_scaleCurve.GetValue(tickManager.tick),
                1
            );
            camera.orthographicSize = 
                (float)_scaleCurve.GetValue(tickManager.tick);
        }
        
        private void Awake()
        {
            instance = this;    
        }

        // set Test Move
        private void Start()
        {
            tickManager = TickManager.instance;
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (!inited) return;
            ModifyCamera();
        }
    }
}