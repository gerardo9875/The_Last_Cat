using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;

public class PerroZombi_movement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D rgb;

    [Header("Player Detection")]
    [SerializeField] float DetectionRadius;
    [SerializeField] float UnfollowDelay;
    [SerializeField] LayerMask playerLayer;
    private float CurrentTime;
    [NonSerialized] public bool Deteccion;

    [Header("Movimiento")]
    [SerializeField] GameObject target;
    [SerializeField] float RunSpeed;
    [SerializeField] float minDistance;
    [NonSerialized] public Vector3 dir;
    [NonSerialized] public bool canMove = true;

    [Header("Ataque Melee")]
    [SerializeField] float attackRadius;
    [SerializeField] Transform attackController;
    [SerializeField] Transform attackRaycastPos;
    [SerializeField] float attackDelay;
    [SerializeField] float passedTime;
    bool canAtack = true;
    [NonSerialized] public bool shouldRotate = true;
    [NonSerialized] public bool isAttacking;

    [Header("Movimiento aleatorio")]
    [SerializeField] float patrolSpeed;
    [SerializeField] float range;
    [SerializeField] BoxCollider2D movSprite;
    private Vector2 wayPoint;
    public float waitTime;
    bool patrolStay;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, DetectionRadius, playerLayer);
    }
    bool AttackRaycast()
    {
        return Physics2D.OverlapCircle(attackRaycastPos.position, attackRadius, playerLayer);
    }

    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        CurrentTime = UnfollowDelay;

        SetNewDestination();

    }

    private void Update()
    {
        if (!Deteccion)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, patrolSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, wayPoint) == 0)
            {
                if(!patrolStay)
                {
                    patrolStay = true;
                    waitTime = UnityEngine.Random.Range(3, 5);

                    StartCoroutine(SetNewDestination());
                }
                

            }
        }


        Detection();
        Movement();
        Rotation();


    }

    private void Movement()
    {
        if (Deteccion && canMove)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, RunSpeed * Time.deltaTime);
            }
            else
            {
                if (canAtack && Attack() != null)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator SetNewDestination()
    {
        yield return new WaitForSeconds(waitTime);

        wayPoint = new Vector2(UnityEngine.Random.Range(movSprite.bounds.min.x, movSprite.bounds.max.x), 
            UnityEngine.Random.Range(movSprite.bounds.min.y, movSprite.bounds.max.y));

        patrolStay = false;
    }


    private void Detection()
    {
        if (PlayerInArea())
        {
            Deteccion = true;
            CurrentTime = UnfollowDelay;
        }
        else
        {
            CurrentTime -= Time.deltaTime;
        }

        if (CurrentTime < 0 && Deteccion)
        {
            Deteccion = false;
        }
    }


    private void Rotation()
    {
        if (shouldRotate)
        {
            dir = target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            attackController.rotation = Quaternion.Euler(0, 0, angle);

            dir.Normalize();
        }
    }


    private IEnumerator Attack()
    {
        isAttacking = true;
        canAtack = false;
        shouldRotate = false;
        canMove = false;

        yield return new WaitForSeconds(passedTime);

        if (AttackRaycast())
        {
            Player_Life life = target.GetComponent<Player_Life>();
            life.currentlife--;
            Debug.Log("Ataque");
        }

        yield return new WaitForSeconds(attackDelay);
        canAtack = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackRaycastPos.position, attackRadius);
    }
}
