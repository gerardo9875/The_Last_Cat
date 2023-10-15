using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Perrozombie_Animcontroller:MonoBehaviour
{
    PerroZombi_movement AI;
    Animator animator;
    SpriteRenderer renderer;

    [Header("Materiales")]
    [SerializeField] Material normalMaterial;
    [SerializeField] Material AttackMaterial;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        AI = GetComponent<PerroZombi_movement>();
    }

    private void Update()
    {
        animator.SetBool("IsWalking", AI.Deteccion);
        animator.SetBool("Attack", AI.isAttacking);

        if (AI.shouldRotate)
        {
            animator.SetFloat("X", AI.dir.x);
            animator.SetFloat("Y", AI.dir.y);
        }

        if (AI.isAttacking) renderer.material = AttackMaterial;
        else renderer.material = normalMaterial;
    }

    public void AttackEnd()
    {
        AI.rgb.velocity = Vector2.zero;
        AI.canMove = true;
        AI.shouldRotate = true;
        AI.isAttacking = false;
    }
}
