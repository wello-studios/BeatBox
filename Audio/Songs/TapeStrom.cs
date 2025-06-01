using System;
using BeatBox.Util;
using Unity.VisualScripting;
using UnityEngine;

namespace BeatBox.Audio.Songs
{
    public class TapeStrom : Song
    {
        private void Awake()
        {
            name = "Tape Strom";
            author = "SLIYA THETA";
            difficulty = Difficulty.Square;
            subDiff = 3;

            bpm = 150;
            
            NoteSpeedCurve.Add(0, CubicBezierCurve.Constant(1));
            GameSpeedCurve.Add(0, CubicBezierCurve.Constant(1));
            
            NoteMapTTArray[0].Add(new [] {12, 0, 1d});
            NoteMapTTArray[0].Add(new [] {13, 0, 1d});
            NoteMapTTArray[1].Add(new [] {14, 0, 1d});
            NoteMapTTArray[1].Add(new [] {15, 0, 1d});
            NoteMapTTArray[2].Add(new [] {16, 0, 1d});
            NoteMapTTArray[2].Add(new [] {17, 0, 1d});
            NoteMapTTArray[3].Add(new [] {18, 0, 1d});
            NoteMapTTArray[3].Add(new [] {19, 0, 1d});
            NoteMapTTArray[5].Add(new [] {20, 0, 1d});
            NoteMapTTArray[5].Add(new [] {21, 0, 1d});
            NoteMapTTArray[6].Add(new [] {22, 0, 1d});
            NoteMapTTArray[6].Add(new [] {23, 0, 1d});
            NoteMapTTArray[7].Add(new [] {24, 0, 1d});
            NoteMapTTArray[7].Add(new [] {25, 0, 1d});
            NoteMapTTArray[4].Add(new [] {26, 0, 1d});
            NoteMapTTArray[4].Add(new [] {27, 0, 1d});
            NoteMapTTArray[0].Add(new [] {32, 0, 1d});
            NoteMapTTArray[0].Add(new [] {33, 0, 1d});
            NoteMapTTArray[1].Add(new [] {34, 0, 1d});
            NoteMapTTArray[1].Add(new [] {35, 0, 1d});
            NoteMapTTArray[2].Add(new [] {36, 0, 1d});
            NoteMapTTArray[2].Add(new [] {37, 0, 1d});
            NoteMapTTArray[3].Add(new [] {38, 0, 1d});
            NoteMapTTArray[3].Add(new [] {39, 0, 1d});
            NoteMapTTArray[5].Add(new [] {40, 0, 1d});
            NoteMapTTArray[5].Add(new [] {41, 0, 1d});
            NoteMapTTArray[6].Add(new [] {42, 0, 1d});
            NoteMapTTArray[6].Add(new [] {43, 0, 1d});
            NoteMapTTArray[7].Add(new [] {44, 0, 1d});
            NoteMapTTArray[7].Add(new [] {45, 0, 1d});
            NoteMapTTArray[4].Add(new [] {46, 0, 1d});
            NoteMapTTArray[4].Add(new [] {47, 0, 1d});
            // NoteMapTTArray[0].Add(new [] {52, 0, 1d});
            // NoteMapTTArray[0].Add(new [] {53, 0, 1d});
            // NoteMapTTArray[1].Add(new [] {54, 0, 1d});
            // NoteMapTTArray[1].Add(new [] {55, 0, 1d});
            // NoteMapTTArray[2].Add(new [] {56, 0, 1d});
            // NoteMapTTArray[2].Add(new [] {57, 0, 1d});
            // NoteMapTTArray[3].Add(new [] {58, 0, 1d});
            // NoteMapTTArray[3].Add(new [] {59, 0, 1d});
            // NoteMapTTArray[5].Add(new [] {60, 0, 1d});
            // NoteMapTTArray[5].Add(new [] {61, 0, 1d});
            // NoteMapTTArray[6].Add(new [] {62, 0, 1d});
            // NoteMapTTArray[6].Add(new [] {63, 0, 1d});
            // NoteMapTTArray[7].Add(new [] {64, 0, 1d});
            // NoteMapTTArray[7].Add(new [] {65, 0, 1d});
            // NoteMapTTArray[4].Add(new [] {66, 0, 1d});
            // NoteMapTTArray[4].Add(new [] {67, 0, 1d});

            
            CamLocationCurves[0].Add(0, CubicBezierCurve.Constant(0));
            //CamLocationCurves[0].Add(100, CubicBezierCurve.Constant(0d));
            CamLocationCurves[1].Add(0, CubicBezierCurve.Constant(0));
            //CamLocationCurves[1].Add(100, CubicBezierCurve.Constant(0d));
            
            CamRotationCurves   .Add(0, CubicBezierCurve.Constant(0));
            //CamRotationCurves   .Add(8, new CubicBezierCurve(8, 128, 0, Math.PI * 300, new Vector2(3, 3)));
            //CamRotationCurves   .Add(128, CubicBezierCurve.Constant(0));
            
            //CamScaleCurves      .Add(0, new CubicBezierCurve(0, 8, 30, 15, new Vector2(3, 3)));
            CamScaleCurves      .Add(0, CubicBezierCurve.Constant(17.5));
        }
    }
}