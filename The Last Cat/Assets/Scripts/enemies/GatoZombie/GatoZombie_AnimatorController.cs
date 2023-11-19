using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    GatoZombi_movement AI;
    GatoZombie_Life life;
    NavMeshAgent agent;

    SpriteRenderer Renderer;
    [SerializeField] Material idleMaterial;
    [SerializeField] Material WalkMaterial;
    [SerializeField] Material ShootMaterial;
    [SerializeField] Material damageMaterial;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        AI = GetComponent<GatoZombi_movement>();
        life = GetComponent<GatoZombie_Life>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;


    }

    void Update()
    {
        animator.SetBool("IsWalking", agent.velocity != Vector3.zero);
        animator.SetBool("Attack", AI.isShooting);
        animator.SetBool("Wet", AI.wet);

        if(life.alive)
        {
            if (AI.Deteccion)
            {
                if (Vector2.Distance(transform.position, agent.steeringTarget) > agent.stoppingDistance)
                {
                    animator.SetFloat("X", agent.velocity.normalized.x);
                    animator.SetFloat("Y", agent.velocity.normalized.y);
                }
                else
                {
                    animator.SetFloat("X", AI.dir.x);
                    animator.SetFloat("Y", AI.dir.y);
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, agent.steeringTarget) > agent.stoppingDistance + 0.3f) //Correr
                {
                    animator.SetFloat("X", agent.velocity.normalized.x);
                    animator.SetFloat("Y", agent.velocity.normalized.y);
                }
                else
                {
                    animator.SetFloat("X", AI.patrolVel.x);
                    animator.SetFloat("Y", AI.patrolVel.y);
                }
            }
        }
        else
        {
            animator.SetFloat("X", AI.lastDir.x);
            animator.SetFloat("Y", AI.lastDir.y);
        }

        if (!life.alive)
        {
            //muerte animacion
        }

        if (agent.velocity != Vector3.zero) Renderer.material = WalkMaterial;
        else if (AI.isShooting) Renderer.material = ShootMaterial;
        else if (AI.wet) Renderer.material = damageMaterial;
        else Renderer.material = idleMaterial;

    }

    public void ShootAnimEnd()
    {
        AI.canMove = true;
        AI.isShooting = false;
    }


    public void EndDead()
    {
        Destroy(gameObject);
    }
}
