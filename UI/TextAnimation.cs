using System;
using BeatBox.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatBox.UI
{
    public class TextAnimation : MonoBehaviour
    {
        public TMP_Text textComponent;
            
        public TMP_FontAsset[] fonts;
        private int _fontNum;
        
        public bool isPlaying;
        public bool playOnAwake;

        public float animSpeed;
        // 0 ~ fontNum-1 //
        private double _animTick;
        
        public CubicBezierCurve curve;
        
        public void Start()
        {
            textComponent.font = fonts[^1];
            if (playOnAwake) StartAnimation();
        }
        
        public void StartAnimation()
        {
            isPlaying = true;
            _animTick = 0;
            textComponent.font = fonts[0];
        }

        public void Update()
        {
            if (isPlaying)
            {
                _animTick += Time.deltaTime * animSpeed;
                if (_animTick >= fonts.Length - 1)
                {
                    isPlaying = false;
                    textComponent.font = fonts[^1];
                }
                
                textComponent.font = fonts[(int)Math.Floor(_animTick)];
            }
        }
    }
}