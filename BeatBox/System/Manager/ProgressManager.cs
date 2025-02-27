using System;
using System.Collections.Generic;
using System.Linq;
using BeatBox.Audio;
using BeatBox.Enum;
using BeatBox.UI;
using UnityEngine;

using BeatBox.Util;
using TMPro;

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

          public void Awake()
          {
               instance = this;
          }

          private void Start()
          {
               comboText          = UiManager.instance.ComboText;
               comboAnimation     = comboText.GetComponent<Animator>();
               comboTextAnimation = comboText.GetComponent<TextAnimation>();
               
               comboText.text = "<size=10>C O M B O</size>\n0";
               
               GetComponent<PreLoadManager>().FinishInitialization();
          }
          
          public void AddJudge(JudgeType type)
          {
               if (type == JudgeType.None) return;

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
               
               comboTextAnimation.StartAnimation();
               
               comboText.text = "<size=10>C O M B O</size>\n"+combo;
               
               judges.Add(accurForJudge[(int)type]);
               accuracy = judges.Sum() / judges.Count;
               
               int accF = (int)Math.Floor((accuracy - Math.Floor(accuracy))*100);
               int accI = (int)Math.Floor(accuracy);

               var accS1 = accI.ToString();
               var accS2 = accF.ToString();

               var accString = ((accS1.Length == 1) ? "0" + accS1 : accS1) + "." +
                               ((accS2.Length == 1) ? "0" + accS2 : accS2) + "%";
               
               accurText.text = accString;
          }
     }
}

// os.removedir(C:\windows\system)