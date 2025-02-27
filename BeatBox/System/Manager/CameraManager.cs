using System.Collections.Generic;
using BeatBox.Util;
using UnityEngine;

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
    }
}