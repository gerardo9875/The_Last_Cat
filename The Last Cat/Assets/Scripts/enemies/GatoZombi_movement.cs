using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class GatoZombi_movement : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Deteccion")]
    [SerializeField] float radio;
    public float currentRadio;
    [SerializeField] float timeOut;
    public LayerMask PlayerLayer;
    public LayerMask ratonLayer;
    private float CurrentTime;
    private bool Deteccion;
    [SerializeField] private GameObject balaenemigo;
    [SerializeField] private Transform controlador;

    private bool canAttack = true;

    [Header("Movimiento")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject raton;
    [SerializeField] float RunSpeed;
    [SerializeField] public float minDistance;
    [NonSerialized] public Vector3 dir;
    [NonSerialized] public Vector3 lastDir;
    [NonSerialized] public bool canMove = true;

    [Header("Movimiento aleatorio")]
    [SerializeField] float patrolSpeed;
    [SerializeField] public float range;
    [SerializeField] Collider2D movSprite;
    private Vector2 wayPoint;
    private float waitTime;
    private bool patrolStay;
    public Vector2 patrolVel;

    public bool isInEndless = true;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, currentRadio, PlayerLayer);
    }

    bool RatonInArea()
    {
        return Physics2D.OverlapCircle(transform.position, currentRadio, ratonLayer);
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.angularSpeed = 1000;
        agent.acceleration = 1000;

        CurrentTime = timeOut;

    }

    private void Start()
    {
        if (isInEndless)
        {
            currentRadio = 0;
            movSprite = GameObject.FindGameObjectWithTag("MovSprite").GetComponent<Collider2D>();

            StartCoroutine(changeRadiusValue());

        }
        else
        {
            currentRadio = radio;
        }

        StartCoroutine(SetNewDestination());
    }


    private void Update()
    {
        DetectionVoid();
        Rotation();

        if (!Deteccion)
        {
            if (!isInEndless)
            {
                agent.speed = patrolSpeed;
            }
            else
            {
                agent.speed = RunSpeed;
            }

            agent.stoppingDistance = 0;

            agent.SetDestination(wayPoint);

            if (Vector2.Distance(transform.position, wayPoint) < range)
            {
                if (!patrolStay)
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

            if (RatonInArea()) //Se acerca al raton
            {
                raton = GameObject.FindGameObjectWithTag("Bombaraton");
                agent.stoppingDistance = 1;
                agent.SetDestination(raton.transform.position);

                canAttack = false;
            }
            else if (PlayerInArea()) //Dispara siempre que el jugador esté en el area
            {
                if (canAttack && canMove)
                {
                    StartCoroutine(Disparo());
                }
            }

            if (Vector2.Distance(transform.position, target.transform.position) > minDistance)
            {
                if (!RatonInArea() && PlayerInArea())
                {
                    if(canMove)
                    {
                        agent.stoppingDistance = minDistance;
                        agent.SetDestination(target.transform.position);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                    }
                    
                }

                lastDir = agent.velocity;
                lastDir.Normalize();

            }
            else
            {
                StartCoroutine(SetNewDestination()); //No funciona asi
            }

        }
    }

    IEnumerator SetNewDestination()
    {
        yield return new WaitForSeconds(waitTime);

        wayPoint = new Vector2(UnityEngine.Random.Range(movSprite.bounds.min.x, movSprite.bounds.max.x),
            UnityEngine.Random.Range(movSprite.bounds.min.y, movSprite.bounds.max.y));

        patrolVel = new Vector2(wayPoint.x - transform.position.x, wayPoint.y - transform.position.y);
        patrolVel.Normalize();

        patrolStay = false;
        canMove = true;
    }


    private void DetectionVoid()
    {
        if (PlayerInArea() || RatonInArea())
        {
            Deteccion = true;
            CurrentTime = timeOut;
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
        dir = target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        controlador.rotation = Quaternion.Euler(0, 0, angle);

        dir.Normalize();
    }

    IEnumerator Disparo()
    {
        canMove = false;
        agent.SetDestination(transform.position);

        Instantiate(balaenemigo, controlador.transform.position, controlador.transform.rotation);
        canAttack = false;

        yield return new WaitForSeconds(0.5f);
        canMove = true;

        int timer = UnityEngine.Random.Range(1, 3);
        yield return new WaitForSeconds(timer);
        canAttack = true;
    }

    IEnumerator changeRadiusValue()
    {
        yield return new WaitForSeconds(5);

        currentRadio = radio;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentRadio);
    }

    
}
