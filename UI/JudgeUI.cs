using BeatBox.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace BeatBox.UI
{
    public class JudgeUI : MonoBehaviour
    {
        public Image JudgeImageComponent;
        public SpriteRenderer JudgeSpriteRenderer;

        public bool isUI;

        // [*] sprite for judgeUI
        /*
         * [0] 360
         * [1] 270
         * [2] 180
         * [3] 90
         * [4] 45
         * [5] 0
         */
        public Sprite[] JudgeSprites;

        public float alpha = 0f;
        public float animSpeed = 1f;

        public void SetJudgeImage(int jt)
        {
            if (jt == 6) return;
            
            if (isUI) JudgeImageComponent.sprite = JudgeSprites[jt];
            else JudgeSpriteRenderer.sprite = JudgeSprites[jt];
        }
        
        public void PulseUI(JudgeType judgeType)
        {
            alpha = 2f;
        }

        public void Update()
        {
            alpha -= Time.deltaTime * animSpeed;
            
            if (isUI) JudgeImageComponent.color = new Color(1f, 1f, 1f, alpha);
            else JudgeSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        }
    }
}