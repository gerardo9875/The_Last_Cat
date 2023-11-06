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
    public bool Deteccion;
    bool mouseDetection;
    [SerializeField] private GameObject balaenemigo;
    [SerializeField] private Transform controlador;

    [NonSerialized] public bool canShoot = true;
    [NonSerialized] public bool isShooting;

    [Header("Movimiento")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject raton;
    [SerializeField] float RunSpeed;
    [SerializeField] public float minDistance;
    [NonSerialized] public Vector3 dir;
    [NonSerialized] public Vector3 lastDir;
    [NonSerialized] public bool canMove = true;

    [Header("Movimiento aleatorio")]
    [SerializeField] float walkSpeed;
    [SerializeField] public float range;
    [SerializeField] Collider2D movSprite;
    private Vector2 wayPoint;
    private float waitTime;
    private bool patrolStay;
    public Vector2 patrolVel;

    [Header("Estado Mojado")]
    public bool wet;
    bool canAim = true;

    [Header("Endless Mode")]
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

        if (!Deteccion)
        {
            if (!isInEndless)
            {
                agent.speed = walkSpeed;
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
                agent.speed = RunSpeed;
                agent.SetDestination(raton.transform.position);

                mouseDetection = true;

            }
            else
            {
                mouseDetection = false;
            }

            if(Vector2.Distance(transform.position, target.transform.position) > minDistance && canMove)
            {
                if(!RatonInArea() && PlayerInArea())
                {
                    agent.stoppingDistance = minDistance;
                    agent.SetDestination(target.transform.position);
                }

                lastDir = dir;
                lastDir.Normalize();
            }

            if (!mouseDetection)
            {
                if (canShoot)
                {
                    agent.SetDestination(transform.position);

                    if (Disparo() != null)
                    {
                        StartCoroutine(Disparo());
                     }
                }
            }
        }

        if (wet)
        {
            canMove = false;
            canShoot = false;
            isShooting = false;
            canAim = false;
        }

        DetectionVoid();
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
        canShoot = false;
        isShooting = true;
        canMove = false;

        yield return new WaitForSeconds(0.55f);

        if (canAim)
        {
            Instantiate(balaenemigo, controlador.transform.position, controlador.transform.rotation);
        }


        int timer = UnityEngine.Random.Range(3, 5);
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }

    IEnumerator changeRadiusValue()
    {
        yield return new WaitForSeconds(5);

        currentRadio = radio;
    }

    public void outOfWater()
    {
        wet = false;
        canAim = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentRadio);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterShoot"))
        {
            wet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("outOfWater", 0.7f);
    }

}
