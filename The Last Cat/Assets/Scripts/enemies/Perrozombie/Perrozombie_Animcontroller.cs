using UnityEngine;
using UnityEngine.AI;

public class Perrozombie_Animcontroller:MonoBehaviour
{
    PerroZombi_movement AI;
    Animator animator;
    SpriteRenderer Renderer;
    NavMeshAgent agent;

    [Header("Materiales")]
    [SerializeField] Material normalMaterial;
    [SerializeField] Material AttackMaterial;

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
        animator.SetBool("IsWalking", AI.Deteccion);
        animator.SetBool("Attack", AI.isAttacking);

        if (Vector2.Distance(transform.position, agent.steeringTarget) > AI.minDistance)
        {
            animator.SetFloat("X", agent.velocity.normalized.x);
            animator.SetFloat("Y", agent.velocity.normalized.y);
        }
        else
        {
            animator.SetFloat("X", AI.lastDir.normalized.x);
            animator.SetFloat("Y", AI.lastDir.normalized.y);
        }

        if (AI.isAttacking) Renderer.material = AttackMaterial;
        else Renderer.material = normalMaterial;
    }

    public void AttackEnd()
    {
        AI.rgb.velocity = Vector2.zero;
        AI.canMove = true;
        AI.canRotate = true;
        AI.isAttacking = false;
    }
}
