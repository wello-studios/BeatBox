using System.Collections.Generic;
using UnityEngine;
using FMOD;
using Debug = UnityEngine.Debug;

namespace BeatBox.Util
{
    public class CubicBezierCurveGroup
    {
        public List<double> Ticks;
        public List<CubicBezierCurve> Curves;

        public CubicBezierCurveGroup(List<double> ticks, List<CubicBezierCurve> curves)
        {
            Ticks = ticks;
            Curves = curves;
        }

        public void Add(double tick, CubicBezierCurve curve)
        {
            Ticks .Add(tick);
            Curves.Add(curve);
        }

        public double GetValue(double tick)
        {
            int currentIndex = -1;
            
            foreach (var T in Ticks)
            {
                if (tick < T) { break; }

                currentIndex++;
            }
            
            return Curves[currentIndex].GetValue(tick);
        }

        public CubicBezierCurve GetCurveWithTick(double tick)
        {
            var i = 0;
            foreach (var T in Ticks)
            {
                if (tick < T) { break; }

                i++;
            }
            return Curves[i-1];
        }
    }
}