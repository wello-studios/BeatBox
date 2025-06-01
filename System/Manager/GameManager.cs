using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BeatBox.Audio;
using BeatBox.Util;

/* { GameManager }
 *
 * [VARIABLE]
 * 생략
 *
 * [METHOD]
 * Pause
 * Resume
 *   게임 정지/재개
 * StartGame
 * EndGame
 *   게임 시작/종료
 * userPause
 * userResume
 *   게임 정지/재개 (유저에 의한)
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH.
 * START - 게임시작
 * UPDATE - 사용자 키입력에 따라 게임 정지/해제
 */

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
          public bool isPaused;
          public bool isUserPauseable = false;
          public int rotateBeforePause = 0;
          public MenuObject pauseMenuObject;
          public Animator pauseAnimator;
          public ParticleSystem[] pauseParticles;
          
          [Header("todo")]
          public int needsToLoad = 0;
          public int LoadedStuff = 0;
          
          [Header("Song Play Curves")]
          public CubicBezierCurveGroup NoteSpeed; // only Const.
          public CubicBezierCurveGroup GameSpeed; // only Const or Slope.

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
               Debug.Log("starting this shit bro");
               
               yield return new WaitForSeconds(0.5f);

               isPaused = false;
               isUserPauseable = true;
          }
          
          public IEnumerator EndGame()
          {
               for (int i = 0; i < 5; i++)
               {
                    yield return new WaitForSeconds(1f);
                    Debug.Log("End Scene Loading in "+ (i + 1)+"seconds");
               }
               
               AudioManager.instance.PauseSong();
               
               SceneManager.LoadScene("End");
          }

          public IEnumerator userPause()
          {
               isUserPauseable = false;
               Cube.Cube.inputManager.isInputable = false;
               pauseAnimator.SetTrigger("pauseKey");

               Pause();
               
               yield return new WaitForSecondsRealtime(1.25f);

               foreach (var p in pauseParticles)
               {
                    p.Play();
               }
               
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
               
               foreach (var p in pauseParticles)
               {
                    p.Stop();
               }
               
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