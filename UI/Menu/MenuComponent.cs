using System;
using UnityEngine;

public class MenuComponent : MonoBehaviour
{
    public Animator animator;
    public bool isActive;
    public MenuObject menuObject;

    public void Activate()
    {
        isActive = true;
        
        animator.SetBool("isactivated", true);
    }

    public void Deactivate()
    {
        isActive = false;
        animator.SetBool("isactivated", false);
    }
}
