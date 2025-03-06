using System;
using UnityEngine;

namespace BeatBox.System.Manager
{
    public class PreLoadManager : MonoBehaviour
    {
        public bool inited = false;
        public GameManager gameManager;

        public void Start()
        {
            gameManager = GameManager.instance;
            gameManager.needsToLoad++;
        }
        
        public void FinishInitialization()
        {
            // if (inited) return;
            // inited = true;
            //
            // gameManager.LoadedStuff++;
            // if (gameManager.needsToLoad == gameManager.LoadedStuff)
            // {
            //     GameManager.instance.Resume();
            // }
        }
    }
}