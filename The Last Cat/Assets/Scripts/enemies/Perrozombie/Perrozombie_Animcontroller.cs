using UnityEngine;
using UnityEngine.AI;

public class Perrozombie_Animcontroller:MonoBehaviour
{
    PerroZombi_movement AI;
    Animator animator;
    SpriteRenderer Renderer;
    NavMeshAgent agent;

    [Header("Materiales")]
    [SerializeField] Material IdleMaterial;
    [SerializeField] Material RunMaterial;
    [SerializeField] Material AttackMaterial;
    [SerializeField] Material DeathMaterial;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        AI = GetComponent<PerroZombi_movement>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        
        animator.SetBool("IsWalking", agent.velocity != Vector3.zero); //Bool para cuando el perro corre
        animator.SetBool("Attack", AI.isAttacking); //Atacando

        if (AI.Deteccion)
        {
            if (Vector2.Distance(transform.position, agent.steeringTarget) > agent.stoppingDistance) //Correr
            {
                animator.SetFloat("X", agent.velocity.normalized.x);
                animator.SetFloat("Y", agent.velocity.normalized.y);
            }
            else //Idle
            {
                animator.SetFloat("X", AI.lastDir.normalized.x);
                animator.SetFloat("Y", AI.lastDir.normalized.y);
            }
        }
        else //Direccion para el estado de patrulla
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

        

        //MATERIALES
        if (AI.isAttacking) Renderer.material = AttackMaterial; //Ataque
        else if (!AI.isAttacking && agent.velocity != Vector3.zero) Renderer.material = RunMaterial; //Corriendo
        else Renderer.material = IdleMaterial; //Ninguna de las anteriores

    }

    public void AttackEnd()
    {
        AI.rgb.velocity = Vector2.zero;
        AI.canMove = true;
        AI.canRotate = true;
        AI.isAttacking = false;
    }
}
