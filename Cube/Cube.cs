
using System;
using Unity.Mathematics;
using UnityEngine;

namespace BeatBox.Cube
{
    public class Cube : MonoBehaviour
    {
        public static CubeInputManager  inputManager;
        public static CubeJudgeManager  judgeManager;
        public static CubeRotateManager rotateManager;
        public static CubeVisualManager visualManager;
        
        
        public static int NormalizeLineNumber(int num, bool minusN = true)
        {
            var result = (int)math.fmod(num, 8);
            if (!minusN) return result;
            return (result < 0) ? 8 + result : result;
        }
        
        private void Awake()
        {
            inputManager  = GetComponent<CubeInputManager>();
            judgeManager  = GetComponent<CubeJudgeManager>();
            rotateManager = GetComponent<CubeRotateManager>();
            visualManager = GetComponent<CubeVisualManager>();
        }
    }
}