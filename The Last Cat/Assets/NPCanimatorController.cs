using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCanimatorController : MonoBehaviour
{
    Animator animator;
    [SerializeField] int num;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("Valor", num);
    }

}
