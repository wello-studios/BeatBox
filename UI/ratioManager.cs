using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratioManager : MonoBehaviour
{
    /**    
    *  0 : TOP
    *  1 : BOTTOM
    *  2 : LEFT
    *  3 : RIGHT
    */
    public GameObject[] Panels;
    
    /** -255 ~ 255 */
    public int[] offsets;

    /**
     * CHANGE THE OFFSET OF INDEX.
     *  0 : TOP
     *  1 : BOTTOM
     *  2 : LEFT
     *  3 : RIGHT
     */
    public void SetOffset(int index, int value)
    {
        offsets[index] = value;
    }
    
    
}
