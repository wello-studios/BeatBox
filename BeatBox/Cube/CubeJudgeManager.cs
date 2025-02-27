using System;
using BeatBox.Cube;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeJudgeManager : MonoBehaviour
{
    public bool[] lineInputHold = new bool[9];
    public bool[] lineInputDown = new bool[9];
    public bool[] lineInputUp   = new bool[9];

    private CubeInputManager  _cim;
    private CubeRotateManager _crm;

    private void Awake()
    {
        _cim = GetComponent<CubeInputManager >();
        _crm = GetComponent<CubeRotateManager>();
    }
    
    private int NormalizeLineNumber(int num) {
        if (num < 0)
        {
            return NormalizeLineNumber(num + 8);
        }
        if (num >= 8)
        {
            return NormalizeLineNumber(num - 8);
        }

        return num;
    }

    private void SetInputOnEachLines()
    {
        for (int i = 0; i < 9; i++)
        {
            lineInputHold[i] = false;
            lineInputDown[i] = false;
            lineInputUp  [i] = false;
        }
        
        // omg this is ~~~ ({[CHILL CODE]}) ~~~ si siht gmo //
        
        lineInputHold[NormalizeLineNumber(_crm.rotateNumber + 0)] = _cim.pressHoldKeyW;
        lineInputHold[NormalizeLineNumber(_crm.rotateNumber + 2)] = _cim.pressHoldKeyA;
        lineInputHold[NormalizeLineNumber(_crm.rotateNumber + 4)] = _cim.pressHoldKeyS;
        lineInputHold[NormalizeLineNumber(_crm.rotateNumber + 6)] = _cim.pressHoldKeyD;
        lineInputHold[8] = _cim.pressHoldKeySpace;
        
        lineInputDown[NormalizeLineNumber(_crm.rotateNumber + 0)] = _cim.pressDownKeyW;
        lineInputDown[NormalizeLineNumber(_crm.rotateNumber + 2)] = _cim.pressDownKeyA;
        lineInputDown[NormalizeLineNumber(_crm.rotateNumber + 4)] = _cim.pressDownKeyS;
        lineInputDown[NormalizeLineNumber(_crm.rotateNumber + 6)] = _cim.pressDownKeyD;
        lineInputDown[8] = _cim.pressDownKeySpace;
        
        lineInputUp  [NormalizeLineNumber(_crm.rotateNumber + 0)] = _cim.pressUpKeyW;
        lineInputUp  [NormalizeLineNumber(_crm.rotateNumber + 2)] = _cim.pressUpKeyA;
        lineInputUp  [NormalizeLineNumber(_crm.rotateNumber + 4)] = _cim.pressUpKeyS;
        lineInputUp  [NormalizeLineNumber(_crm.rotateNumber + 6)] = _cim.pressUpKeyD;
        lineInputUp  [8] = _cim.pressUpKeySpace;
    }
    
    private void Update()
    {
        SetInputOnEachLines();
    }
}