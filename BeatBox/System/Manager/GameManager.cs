using System;
using System.Collections;
using BeatBox.Audio;
using UnityEngine;

using BeatBox.Util;

namespace BeatBox.System.Manager
{
     public class GameManager : MonoBehaviour
     {
          [Header("Components")]
          public static GameManager instance;
          public TickManager tickManager;
          public NoteManager noteManager;
          public AudioManager audioManager;
          public CameraManager cameraManager;

          [Header("Song Data")]
          public string songName;
          public string author;
          public Difficulty difficulty;
          
          [Header("Pause Menu")]
          public bool isPaused = false;
          public bool isUserPauseable = false;
          public int rotateBeforePause = 0;
          public MenuObject pauseMenuObject;
          public Animator pauseAnimator;
          
          [Header("todo;")]
          public int needsToLoad = 0;
          public int LoadedStuff = 0;
          
          // only const or slope; ( cubic equation is fking hard )
          [Header("Song Play Curves")]
          public CubicBezierCurveGroup NoteSpeed;
          public CubicBezierCurveGroup GameSpeed;

          [Header("Game Settings")]
          public int startTick = 8;
          public double noteSpawnPos = 40;
          
          public void Pause()
          {
               audioManager.PauseSong();
               
               isPaused = true;
               rotateBeforePause = Cube.Cube.rotateManager.rotateNumber;
          }

          public void Resume()
          {
               audioManager.ResumeSong();
               
               isPaused = false;
               Cube.Cube.rotateManager.rotateNumber = rotateBeforePause;
          }

          public IEnumerator StartGame()
          {
               
               yield return new WaitForSeconds(0.5f);

               isPaused = false;
               isUserPauseable = true;
          }

          public IEnumerator userPause()
          {
               isUserPauseable = false;
               Cube.Cube.inputManager.isInputable = false;
               pauseAnimator.SetTrigger("pauseKey");

               Pause();
               
               yield return new WaitForSecondsRealtime(1.25f);
               
               MenuManager.instance.ChangeMenu(pauseMenuObject);
               Cube.Cube.inputManager.isInputable = true;
               isUserPauseable = true;
          }
          
          public IEnumerator userResume()
          {
               isUserPauseable = false;
               Cube.Cube.inputManager.isInputable = false;
               Cube.Cube.rotateManager.rotateNumber = rotateBeforePause;
               MenuManager.instance.DeselectMenu();
               pauseAnimator.SetTrigger("pauseKey");
               
               yield return new WaitForSecondsRealtime(3);

               Resume();
               Cube.Cube.inputManager.isInputable = true;
               isUserPauseable = true;
          }

          private void Awake()
          {
               instance = this;

               isPaused = true;
               isUserPauseable = false;
          }

          private void Start()
          {
               StartCoroutine(StartGame());
          }

          private void Update()
          {
               if (Cube.Cube.inputManager.pressDownKeyEscape && isUserPauseable)
               {
                    if (!isPaused) StartCoroutine(userPause());
                    else if (isPaused) StartCoroutine(userResume());
               }
          }
     }
}

// i want to hit uengs fucking face 'till die
// mq is right
// my english is 