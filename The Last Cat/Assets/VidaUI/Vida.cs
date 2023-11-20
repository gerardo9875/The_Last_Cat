using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public Animator[] animator;

    public void Life(int actualizar) 
    {
        for(int i = 0; i < animator.Length; i++) 
        {
            animator[i].SetBool("Active", i >= actualizar);
        }   
    }
}
