using System;
using System.Collections.Generic;
using BeatBox.Enum;
using BeatBox.Note;
using BeatBox.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace BeatBox.System.Manager
{
    public class JudgeManager : MonoBehaviour
    {
        public static JudgeManager instance;
        // public static List<double>[] NoteTicks = new List<double>[9];
        
        public GameManager gameManager;
        public TickManager tickManager;
        
        public JudgeUI[] judgeUIs;
        
        // 360 270 180 90 45 0
        // +-기준, second
        public static double[] JudgeOffsets = new []
        {
            0.05,   // 360 100
            0.1,    // 270  80
            0.15,   // 180  60
            0.2,    // 90   40
            0.3,    // 45   20
            1.00,   // 0     0
        };

        private void Awake()
        {
            instance = this;
        }

        public static void Init(float BPM)
        {
            for (int i = 0; i < 6; i++)
            {
                JudgeOffsets[1] /= BPM/60f;
            }
        }

        private void Start()
        {
            gameManager = GameManager.instance;
            tickManager = TickManager.instance;
        }

        public static BBInputType GetInputType(NoteType nt)
        {
            switch (nt)
            {
                case NoteType.Normal:
                case NoteType.Power:
                    return BBInputType.Down;
                
                case NoteType.Active:
                case NoteType.Metronome:
                    return BBInputType.Hold;
                default:
                    return BBInputType.NotJudgeAble;
            }
        }

        public static JudgeType GetJudgeType(double noteTick, double multiplier)
        {
            for (int i = 0; i < JudgeOffsets.Length; i++)
            {
                if (
                    Mathf.Abs((float)(noteTick - TickManager.instance.tick)) <
                    JudgeOffsets[i]
                    / GameManager.instance.GameSpeed.GetValue(TickManager.instance.tick)
                    / GameManager.instance.NoteSpeed.GetValue(TickManager.instance.tick)
                )
                {
                    return (JudgeType)i;
                }
            }

            return JudgeType.None;
        }

        public static void ApplyJudge(JudgeType judgeType)
        {
            if (judgeType == JudgeType.None) return;
            
            Progressmanager.instance.AddJudge(judgeType);
            
            foreach (JudgeUI judgeUI in instance.judgeUIs)
            {
                judgeUI.SetJudgeImage((int)judgeType);
                judgeUI.PulseUI(judgeType);
            }
        }
    }
}