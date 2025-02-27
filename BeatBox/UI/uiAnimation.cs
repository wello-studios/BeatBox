using BeatBox.Util;
using UnityEngine;

namespace BeatBox.UI
{
    public class uiAnimation : MonoBehaviour
    {
        public RectTransform rectTransform;

        public Vector3 startPosition;
        public Vector3 endPosition;
        
        public Vector3 startSize;
        public Vector3 endSize;
        
        public bool isPlaying;
        public bool playOnAwake;

        public float animSpeed;
        private double animTick;

        // t0~1; v0~1;
        public bool isLinear;
        public bool useTwoCurves;
        public CubicBezierCurve curve;
        public CubicBezierCurve curveX;
        public CubicBezierCurve curveY;

        public void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            
            endPosition = rectTransform.position;
            if (playOnAwake) StartAnimation();
        }

        public double GetInternalConflictPointPosition(double r, double p, double q)
        {
            if (p.Equals(q)) return q;
            if (r.Equals(0)) return p;
            
            // |----r-----|---r2---|
            // p----------x--------q
            
            double r2 = 1 - r;
            double result = r*q + r2*p; // r + r2 = 1;

            return result;
        }

        public void StartAnimation()
        {
            isPlaying = true;
            animTick = 0;
            rectTransform.position = startPosition;
        }
        
        public void Update()
        {
            if (isPlaying)
            {
                if (animTick >= 1)
                {
                    rectTransform.position = endPosition;
                    isPlaying = false;
                }

                animTick += Time.deltaTime * animSpeed;

                double pos1;
                double pos2;

                if (isLinear)
                {
                    pos1 = GetInternalConflictPointPosition(startPosition.x, endPosition.x, animTick);
                    pos2 = GetInternalConflictPointPosition(startPosition.y, endPosition.y, animTick);
                }
                else if (useTwoCurves)
                {
                    pos1 = GetInternalConflictPointPosition(startPosition.x, endPosition.x, curveX.GetValue(animTick));
                    pos2 = GetInternalConflictPointPosition(startPosition.y, endPosition.y, curveY.GetValue(animTick));
                }
                else
                {
                    pos1 = GetInternalConflictPointPosition(startPosition.x, endPosition.x, curve.GetValue(animTick));
                    pos2 = GetInternalConflictPointPosition(startPosition.y, endPosition.y, curve.GetValue(animTick));
                }
                
                rectTransform.position = new Vector3((float)pos1, (float)pos2, 0);
            }
        }
    }
}