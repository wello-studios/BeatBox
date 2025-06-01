using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeatBox.Util
{
    public class Equations
    {
                // 삼차방정식 : with help from CHATGPT
        public static List<double> SolveCubic(double a, double b, double c, double d)
        {
            List<double> roots = new List<double>();

            // a가 0이면 이 방정식은 삼차방정식이 아니라 이차방정식이므로 처리.
            if (a == 0)
            {
                Debug.Log("This is not a cubic equation(a==0) : "
                    +a+"x3 + "+b+"x2 + "+c+"x + "+d+" = 0");
                return roots;
            }

            // 삼차방정식의 계수 정규화
            double A = b / a;
            double B = c / a;
            double C = d / a;

            double Q = (3 * B - A * A) / 9;
            double R = (9 * A * B - 27 * C - 2 * A * A * A) / 54;
            double D = Q * Q * Q + R * R;  // 판별식

            // 실수 해를 가질 수 있는지 확인
            if (D > 0)
            {
                // 삼차방정식이 하나의 실근을 가짐
                double S = Math.Cbrt(R + Math.Sqrt(D));
                double T = Math.Cbrt(R - Math.Sqrt(D));

                double root = (S + T) - A / 3;
                roots.Add(root);
            }
            else if (D == 0)
            {
                // 삼차방정식이 중근을 가짐
                double S = Math.Cbrt(R);
                double root1 = 2 * S - A / 3;
                double root2 = -S - A / 3;
                roots.Add(root1);
                roots.Add(root2);
            }
            else
            {
                // 삼차방정식이 세 개의 실근을 가짐
                double theta = Math.Acos(R / Math.Sqrt(-Q * Q * Q));
                double root1 = 2 * Math.Sqrt(-Q) * Math.Cos(theta / 3) - A / 3;
                double root2 = 2 * Math.Sqrt(-Q) * Math.Cos((theta + 2 * Math.PI) / 3) - A / 3;
                double root3 = 2 * Math.Sqrt(-Q) * Math.Cos((theta + 4 * Math.PI) / 3) - A / 3;

                roots.Add(root1);
                roots.Add(root2);
                roots.Add(root3);
            }

            // 실수 근만 필터링
            return roots.Where(root => !double.IsNaN(root) && !double.IsInfinity(root)).ToList();
        }

        // 이차방정식        
        public static List<double> SolveQuadratic(double a, double b, double c)
        {
            List<double> roots = new List<double>();
            var D = Math.Pow(b, 2) - (4 * a * c);
            
            if (D > 0)
            {
                roots.Add((-b + Math.Sqrt(D)) / (2 * a));
                roots.Add((-b - Math.Sqrt(D)) / (2 * a));
            }

            if (D == 0)
            {
                roots.Add(-b / (2 * a));
            }

            return roots;
        }

        // 일차방정식
        public static double SolveLinear(double a, double b)
        {
            return b / a;
        }
    }
}