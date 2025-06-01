using System;
using Unity.Mathematics;
using UnityEngine;

namespace BeatBox.Util
{
    public class CubicBezierCurve
    {
        public double startTick;
        public double endTick;
        public double startValue;
        public double endValue;
        public Vector2 data1;
        public Vector4 data2;

        public bool isConstant;
        public bool isLinear;
        public bool isSin;
        public bool isCos;
        
        // t1, t2 : start tick, end tick;
        // data = (1, 2) -> Y = X;
        // https://www.desmos.com/calculator?lang=ko
        //f\left(x\right)=0\left(1-x\right)^{3\ }+a\left(1-x\right)^{2}x+b\left(1-x\right)x^{2}+x^{3}\left\{0<x<1\right\}
        public CubicBezierCurve(double startTick, double endTick, double startValue, double endValue, Vector2 data)
        {
            this.startTick = startTick;
            this.endTick = endTick;
            this.startValue = startValue;
            this.endValue = endValue;
            this.data1 = data;
        }
        
        public CubicBezierCurve(double startTick, double endTick, double startValue, double endValue, Vector4 data)
        {
            this.startTick = startTick;
            this.endTick = endTick;
            this.startValue = startValue;
            this.endValue = endValue;
            this.data2 = data;
        }
        
        public static CubicBezierCurve Constant(double value)
        {
            var result = new CubicBezierCurve(0, 1, value, value, Vector2.zero)
            {
                isConstant = true
            };

            return result;
        }

        public static CubicBezierCurve Linear(double startTick, double endTick, double startValue, double endValue)
        {
            var result = new CubicBezierCurve(startTick, endTick, startValue, endValue, Vector2.zero) 
            {
                isLinear = true
            };

            return result;
        }

        // data = {height, frequency, offset, absolute(0:off,1:on)}
        // f\left(x\right)=a\left|\sin\frac{\pi}{b}\left(x-d\right)\right|+c
        // a|sin(pi/b(x-d))|+c
        public static CubicBezierCurve Sin(double startTick, double endTick, Vector4 data)
        {
            var result = new CubicBezierCurve(startTick, endTick, 0, 0, data)
            {
                isSin = true
            };
            
            return result;
        }
        
        public static CubicBezierCurve Cos(double startTick, double endTick, Vector4 data)
        {
            var result = new CubicBezierCurve(startTick, endTick, 0, 0, data)
            {
                isCos = true
            };
            
            return result;
        }

        public double GetValue(double tick)
        {
            if (isConstant)
            {
                return startValue;
            }
            
            var normalizedTick = (tick - startTick) / ( endTick - startTick );

            if (isLinear)
            {
                var p =
                    (endValue - startValue) / (endTick - startTick);
                var q = -startTick * p + startValue;
                
                return p * tick + q;
            }

            // data = {height, frequency, offset, absolute(0:off,1:on)}
            if (isSin)
            {
                var a = data2.x;
                var b = data2.y;
                var c = data2.z;
                var d = data2.w;
                
                var inValue = Mathf.Sin((Mathf.PI / b) * (float)(tick - startTick)) + c;
                var result = a * ((d == 0) ? inValue : Mathf.Abs(inValue));

                return result;
            }
            
            if (isCos)
            {
                var a = data2.x;
                var b = data2.y;
                var c = data2.z;
                var d = data2.w;
                
                var inValue = Mathf.Cos((Mathf.PI / b) * (float)(tick - startTick));
                var result = a * ((d == 0) ? inValue : Mathf.Abs(inValue));

                return result;
            }
            
            return (
            ( data1.x * math.pow(1 - normalizedTick, 2) * normalizedTick
             + data1.y * (1 - normalizedTick) * math.pow(normalizedTick, 2)
             + math.pow(normalizedTick, 3)
            ) * (endValue - startValue) + startValue);
        }
    }
}