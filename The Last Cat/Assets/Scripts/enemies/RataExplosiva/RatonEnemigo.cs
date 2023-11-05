using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class RatonEnemigo : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;

    [Header("Detección")]
    public float radioDetec;
    public Transform player;
    public LayerMask PlayerLayer;
    private bool detector;

    [Header("Movimiento")]
    public float velocidad;
    public bool canMove = true;
    Vector2 dir;
    bool canRotate = true;

    [Header("Explosion")]
    public GameObject Explosion;
    public float radioExpl;
    public float ContadorTiempo;

    [Header("Contador de enemigos")]
    public bool toCount = true;
    bool canAdd = true;


    bool ExplodingArea()
    {
        return Physics2D.OverlapCircle(transform.position, radioExpl, PlayerLayer);
    }

    bool PlayerDetetion()
    {
        return Physics2D.OverlapCircle(transform.position, radioDetec, PlayerLayer);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.angularSpeed = 1000;
        agent.acceleration = 20;
        agent.speed = velocidad;

    }

    void Update()
    {
        dir = agent.velocity;
        dir.Normalize();

        if (canRotate)
        {
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
        }


        animator.SetBool("IsWalking", detector);
        animator.SetBool("Exploding", ExplodingArea());

        Detection();

        if (ExplodingArea())
        {
            canMove = false;
            canRotate = false;
        }

        if (detector && canMove)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }


    private void Detection()
    {
        if (PlayerDetetion())
        {
            detector = true;
        }
    }


    public void ExplosionVoid()
    {
        canMove= false;

        if (Explosion != null && canAdd)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            canAdd = false;
        }

        if (toCount)
        {
            if (GameObject.Find("Estacion") != null)
            {
                EstationStop station = GameObject.Find("Estacion").GetComponent<EstationStop>();
                station.enemyCounter++;
            }
            else if (GameObject.Find("EnemyCounter") != null)
            {
                EnemyCounter counter = GameObject.Find("EnemyConuter").GetComponent<EnemyCounter>();
                counter.addEnemy();
            }
        }

        Destroy(gameObject);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExpl);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDetec);
    }

}
