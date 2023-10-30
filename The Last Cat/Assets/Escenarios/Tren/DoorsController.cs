using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] GameObject[] puertas;

    public bool open;

    private void Update()
    {
        if (open)
        {
            for (int i = 0; i < puertas.Length; i++)
            {
                Animator anim = puertas[i].GetComponent<Animator>();

                anim.SetBool("Open", open);
            }
        }
        else
        {
            for (int i = 0; i < puertas.Length; i++)
            {
                Animator anim = puertas[i].GetComponent<Animator>();

                anim.SetBool("Open", open);
            }
        }
        
    }
}
