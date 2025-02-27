using BeatBox.Audio;
using UnityEngine;

using BeatBox.Util;

namespace BeatBox.System.Manager
{
     public class GameManager : MonoBehaviour
     {
          public static GameManager instance;
          public TickManager tickManager;
          public NoteManager noteManager;
          public AudioManager audioManager;
          public CameraManager cameraManager;

          public string songName;
          public string author;
          public Difficulty difficulty;
          
          public bool isPaused = true;
          public int rotateBeforePause = 0;

          public int needsToLoad = 0;
          public int LoadedStuff = 0;
          
          // only const or slope; ( cubic equation is fking hard )
          public CubicBezierCurveGroup NoteSpeed;
          public CubicBezierCurveGroup GameSpeed;

          public int startTick = 8;
          public double noteSpawnPos = 40;
          
          public void Pause()
          {
               isPaused = true;
               rotateBeforePause = Cube.Cube.rotateManager.rotateNumber;
          }

          public void Resume()
          {
               isPaused = false;
               Cube.Cube.rotateManager.rotateNumber = rotateBeforePause;
          }

          private void Awake()
          {
               instance = this;

               isPaused = true;
          }
     }
}

// i want to hit uengs fucking face 'till die
// mq is right
// my english is 