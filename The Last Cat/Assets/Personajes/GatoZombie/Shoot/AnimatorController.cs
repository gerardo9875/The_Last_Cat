using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorController : MonoBehaviour
{
   public Animator animator;
 public    GatoZombi_movement gatoZombie;
   public SpriteRenderer Renderer;
 public   NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gatoZombie= GetComponent<GatoZombi_movement>();
        //life = GetComponent <>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        animator.SetBool("IsWalking", agent.velocity != Vector3.zero);
        animator.SetBool("Attack", gatoZombie.Attack);



        
            animator.SetFloat("X", agent.velocity.normalized.x);
            animator.SetFloat("Y", agent.velocity.normalized.y);
        
    }


    public void EndDead()
    {
        Destroy(gameObject);
    }
}
