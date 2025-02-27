
using System;
using UnityEngine;

namespace BeatBox.Cube
{
    public class Cube : MonoBehaviour
    {
        public static CubeInputManager  inputManager;
        public static CubeJudgeManager  judgeManager;
        public static CubeRotateManager rotateManager;
        public static CubeVisualManager visualManager;
        
        private void Awake()
        {
            inputManager  = GetComponent<CubeInputManager>();
            judgeManager  = GetComponent<CubeJudgeManager>();
            rotateManager = GetComponent<CubeRotateManager>();
            visualManager = GetComponent<CubeVisualManager>();
        }
    }
}