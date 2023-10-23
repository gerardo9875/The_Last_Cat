using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class PerroZombi_movement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D rgb;
    NavMeshAgent agent;

    [Header("Player Detection")]
    [SerializeField] float DetectionRadius;
    [SerializeField] float UnfollowDelay;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask ratonLayer;
    private float CurrentTime;
    [NonSerialized] public bool Deteccion;

    [Header("Movimiento")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject raton;
    [SerializeField] float RunSpeed;
    [SerializeField] public float minDistance;
    [NonSerialized] public Vector3 dir;
    [NonSerialized] public Vector3 lastDir;
    [NonSerialized] public bool canMove = true;

    [Header("Ataque Melee")]
    [SerializeField] float attackRadius;
    [SerializeField] Transform attackController;
    [SerializeField] Transform attackRaycastPos;
    [SerializeField] float attackDelay;
    [SerializeField] float passedTime;
    private bool canAtack = true;
    [NonSerialized] public bool canRotate = true;
    [NonSerialized] public bool isAttacking;

    [Header("Movimiento aleatorio")]
    [SerializeField] float patrolSpeed;
    [SerializeField] float range;
    [SerializeField] Collider2D movSprite;
    private Vector2 wayPoint;
    private float waitTime;
    private bool patrolStay;
    [NonSerialized] public Vector2 patrolVel;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, DetectionRadius, playerLayer);
    }

    bool RatonInArea()
    {
        return Physics2D.OverlapCircle(transform.position, DetectionRadius, ratonLayer);
    }

    bool AttackRaycast()
    {
        return Physics2D.OverlapCircle(attackRaycastPos.position, attackRadius, playerLayer);
    }



    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.angularSpeed = 1000;
        agent.acceleration = 1000;

        CurrentTime = UnfollowDelay;

    }
    private void Start()
    {
        StartCoroutine(SetNewDestination());
    }

    private void Update()
    {
        if (!Deteccion)
        {
            agent.speed = patrolSpeed;
            agent.stoppingDistance = 0;

            agent.SetDestination(wayPoint);

            if (Vector2.Distance(transform.position, wayPoint) < range)
            {
                if(!patrolStay)
                {
                    patrolStay = true;
                    waitTime = UnityEngine.Random.Range(3, 5);

                    StartCoroutine(SetNewDestination());
                }
                

            }
        }
        else
        {
            agent.speed = RunSpeed;
            agent.stoppingDistance = minDistance;

            if (Vector2.Distance(transform.position, target.transform.position) > minDistance && canMove)
            {
                if (RatonInArea())
                {
                    raton = GameObject.FindGameObjectWithTag("Bombaraton");
                    agent.SetDestination(raton.transform.position);
                }
                else if(!RatonInArea() && PlayerInArea())
                {
                    agent.SetDestination(target.transform.position);
                }


                lastDir = agent.velocity;
                lastDir.Normalize();

            }
            else
            {
                agent.SetDestination(transform.position);

                if (canAtack && Attack() != null)
                {
                    StartCoroutine(Attack());
                }
            }
        }

        RatonDetection();
        PlayerDetection();
        Rotation();

    }

    IEnumerator SetNewDestination()
    {
        yield return new WaitForSeconds(waitTime);

        wayPoint = new Vector2(UnityEngine.Random.Range(movSprite.bounds.min.x, movSprite.bounds.max.x), 
            UnityEngine.Random.Range(movSprite.bounds.min.y, movSprite.bounds.max.y));

        patrolVel = new Vector2(wayPoint.x - transform.position.x, wayPoint.y - transform.position.y);
        patrolVel.Normalize();

        patrolStay = false;
    }


    private void PlayerDetection()
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

    private void RatonDetection()
    {
        if (RatonInArea())
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
        if (canRotate)
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
        canRotate = false;
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
