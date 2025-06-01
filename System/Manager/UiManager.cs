using System;
using BeatBox.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* { UI Manager }
 *
 * [VARIABLE]
 * TMP_Text ComboText
 * TMP_Text ScoreText
 * Image JudgeImage
 *
 * [METHOD]
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH.
 * START - 
 * UPDATE - 
 */

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
    }
}