using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BeatBox.Audio;
using BeatBox.Enum;
using BeatBox.UI;
using UnityEngine;

using BeatBox.Util;
using TMPro;

/* { ProgressManager }
 *
 * [VARIABLE]
 * 생략
 *
 * [METHOD]
 * void AddJudge (JudgeType type)
 *   게임에 판정 적용
 *   콤보 재계산
 *   점수 계산
 *   게임 종료 검토
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH.
 * START - UI 초기화
 * UPDATE - 
 */

namespace BeatBox.System.Manager
{
     public class Progressmanager : MonoBehaviour
     {
          public static Progressmanager instance;

          public int combo;

          public double score;
          public double maxScore = 750000d;
          
          public float accuracy;
          public List<float> judges = new ();

          public double[] scoreForJudge = new double[6];
          public int[]    accurForJudge = new int[6];

          public TMP_Text comboText;
          public Animator comboAnimation;
          public TextAnimation comboTextAnimation;

          public TMP_Text accurText;
          public TMP_Text scoreText;

          public void AddJudge(JudgeType type)
          {
               if (type == JudgeType.None) return;

               // COMBO //
               if (type == JudgeType.D0)
               {
                    combo = 0;
                    comboAnimation.SetTrigger("Break");
               }
               else
               {
                    combo++;
                    comboAnimation.SetTrigger("Combo");
               }
               
               // COMBO:TEXT //
               comboTextAnimation.StartAnimation();
               comboText.text = "<size=10>C O M B O</size>\n"+combo;
               
               // JUDGE & ACCURE:TEXT //
               judges.Add(accurForJudge[(int)type]);
               accuracy = judges.Sum() / judges.Count;
               
               int accF = (int)Math.Floor((accuracy - Math.Floor(accuracy))*100); // 소수부분
               int accI = (int)Math.Floor(accuracy);                              // 정수부분

               var accFS = accI.ToString();
               var accFI = accF.ToString();

               var accString = ((accFS.Length == 1) ? "0" + accFS : accFS) + "." +
                               ((accFI.Length == 1) ? "0" + accFI : accFI) + "%";
               
               accurText.text = accString;

               // SCORE //
               var scoreWillAdded = scoreForJudge[(int)type];
               score += scoreWillAdded;
               
               // SCORE:TEXT //
               string scoreT = ((int)Math.Floor(score)).ToString();
               
               scoreText.text = 
                    (scoreT.Length >= 4)?
                         (scoreT.Substring(0, scoreT.Length - 3) + "," + scoreT.Substring(scoreT.Length - 3)) :
                         scoreT;

               // CHECK IS GAME ENDED //
               if (judges.Count >= NoteManager.instance.noteCount)
               {
                    Debug.Log("Game END!!!!");
                    StartCoroutine(GameManager.instance.EndGame());
               }
          }
          
          public void Awake()
          {
               instance = this;
          }

          private void Start()
          {
               comboText.text = "<size=10>C O M B O</size>\n0";
               accurText.text = "00.00%";
               scoreText.text = "0";
               
               //GetComponent<PreLoadManager>().FinishInitialization();
          }
     }
}

// os.removedir(C:\windows\system)