using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RatonEnemigo : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rgb;

    [Header("Detección")]
    public float radioDetec;
    public Transform player;
    public LayerMask PlayerLayer;
    private bool detector;

    [Header("Movimiento")]
    public float velocidad;
    private bool canMove = true;
    Vector2 dir;
    bool canRotate = true;

    [Header("Explosion")]
    public GameObject Explosion;
    public float radioExpl;
    public float ContadorTiempo;


    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, radioExpl, PlayerLayer);
    }

    bool PlayerDetetion()
    {
        return Physics2D.OverlapCircle(transform.position, radioDetec, PlayerLayer);
    }

    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        dir = player.position - transform.position;
        dir.Normalize();

        if (canRotate)
        {
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
        }


        animator.SetBool("IsWalking", detector);
        animator.SetBool("Exploding", PlayerInArea());

        Detection();

        if (PlayerInArea())
        {
            canMove = false;
            canRotate = false;
        }
    }


    private void Detection()
    {
        if (PlayerDetetion())
        {
            detector = true;
        }

        if (detector && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidad * Time.deltaTime);
        }
    }


    public void ExplosionVoid()
    {
        canMove= false;

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
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
