using System;
using BeatBox.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatBox.System.Manager
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        
        public TMP_Text ComboText;
        public TMP_Text ScoreText;

        public Image JudgeImage;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GetComponent<PreLoadManager>().FinishInitialization();
        }
    }
}