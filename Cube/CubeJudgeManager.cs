using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using BeatBox.Cube;

/* CubeJudgeManager
 * 각각의 Line에 키입력 여부를 제어한다.
 *
 * [VARIABLE]
 * bool[9] lineInputHold
 * bool[9] lineInputDown
 * bool[9] lineInputUp
 *   각 라인의 입력종류당 입력 여부.
 *
 * [METHOD]
 * void SetInputOnEachLine
 *   키 입력과 큐브의 돌아간 정도를 종합해 변수의 값을 변경한다.
 * 
 * [UNITY EVENT]
 * AWAKE - CIM, CRM을 불러온다.
 * START - 
 * UPDATE - SetInputOnEachLine을 호출한다.
 */

public class CubeJudgeManager : MonoBehaviour
{
    
    /*[ VARIABLE ]*/
    public bool[] lineInputHold = new bool[9];
    public bool[] lineInputDown = new bool[9];
    public bool[] lineInputUp   = new bool[9];

    private CubeInputManager  _cim;
    private CubeRotateManager _crm;

    /*[ METHOD ]*/
    private void SetInputOnEachLine()
    {
        for (int i = 0; i < 9; i++)
        {
            lineInputHold[i] = false;
            lineInputDown[i] = false;
            lineInputUp  [i] = false;
        }
        
        Debug.Log(Cube.NormalizeLineNumber(_crm.rotateNumber + 0));
        Debug.Log(Cube.NormalizeLineNumber(_crm.rotateNumber + 2));
        Debug.Log(Cube.NormalizeLineNumber(_crm.rotateNumber + 4));
        Debug.Log(Cube.NormalizeLineNumber(_crm.rotateNumber + 6));
        Debug.Log("=============================");
        
        lineInputHold[Cube.NormalizeLineNumber(_crm.rotateNumber + 0)] = _cim.pressHoldKeyW;
        lineInputHold[Cube.NormalizeLineNumber(_crm.rotateNumber + 2)] = _cim.pressHoldKeyA;
        lineInputHold[Cube.NormalizeLineNumber(_crm.rotateNumber + 4)] = _cim.pressHoldKeyS;
        lineInputHold[Cube.NormalizeLineNumber(_crm.rotateNumber + 6)] = _cim.pressHoldKeyD;
        lineInputHold[8] = _cim.pressHoldKeySpace;
        
        lineInputDown[Cube.NormalizeLineNumber(_crm.rotateNumber + 0)] = _cim.pressDownKeyW;
        lineInputDown[Cube.NormalizeLineNumber(_crm.rotateNumber + 2)] = _cim.pressDownKeyA;
        lineInputDown[Cube.NormalizeLineNumber(_crm.rotateNumber + 4)] = _cim.pressDownKeyS;
        lineInputDown[Cube.NormalizeLineNumber(_crm.rotateNumber + 6)] = _cim.pressDownKeyD;
        lineInputDown[8] = _cim.pressDownKeySpace;
        
        lineInputUp  [Cube.NormalizeLineNumber(_crm.rotateNumber + 0)] = _cim.pressUpKeyW;
        lineInputUp  [Cube.NormalizeLineNumber(_crm.rotateNumber + 2)] = _cim.pressUpKeyA;
        lineInputUp  [Cube.NormalizeLineNumber(_crm.rotateNumber + 4)] = _cim.pressUpKeyS;
        lineInputUp  [Cube.NormalizeLineNumber(_crm.rotateNumber + 6)] = _cim.pressUpKeyD;
        lineInputUp  [8] = _cim.pressUpKeySpace;
    }
    
    /*[ UNITY EVENT ]*/
    private void Awake()
    {
        _cim = GetComponent<CubeInputManager >();
        _crm = GetComponent<CubeRotateManager>();
    }
    
    private void Update()
    {
        SetInputOnEachLine();
    }
}