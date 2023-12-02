using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public GameObject[] vidas;

    public void Life(int actualizar) 
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            Animator anim = vidas[i].GetComponent<Animator>();
            if (anim.enabled == true)
            {
                anim.SetBool("Active", i >= actualizar);
            }
        }   
    }

    public void NewLIfe()
    {
        for(int i = 0;i < vidas.Length;i++)
        {
            Animator anim = vidas[i].GetComponent<Animator>();
            anim.SetBool("Active", false);

            if (vidas[i].activeInHierarchy == false)
            {
                vidas[i].SetActive(true);
                break;
            }
        }
    }
}
