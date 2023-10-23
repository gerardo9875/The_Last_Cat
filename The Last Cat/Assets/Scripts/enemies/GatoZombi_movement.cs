using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GatoZombi_movement : MonoBehaviour
{
    [Header("Deteccion")]
    [SerializeField] float radio;
    [SerializeField] float timeOut;
    public LayerMask PlayerLayer;
    public LayerMask ratonLayer;
    private float CurrentTime;
    private bool Deteccion;
    [SerializeField] private GameObject balaenemigo;
    [SerializeField] private Transform controlador;

    private Vector3 dir;
    private bool canAttack = true;

    [Header("Movimiento")]
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float minDistance;

    [Header("Movimiento aleatorio")]
    public float speedE;
    public float range;//Lo cerca que debe estar del punto inicial
    public float maxDistance;//Lejos puede llegar

    private Vector2 wayPoint;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, radio, PlayerLayer);
    }

    bool RatonInArea()
    {
        return Physics2D.OverlapCircle(transform.position, radio, ratonLayer);
    }


    private void Start()
    {
        CurrentTime = timeOut;
        SetNewDestination();
    }


    private void Update()
    {

        if (PlayerInArea() == false)
        {
           
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, speedE * Time.deltaTime);
            if (Vector2.Distance(transform.position, wayPoint) < range)
            {
                SetNewDestination();
            }
        }

        else
        {
            Movement();
            Detection();
            Rotation();

            if (canAttack && Disparo() != null) StartCoroutine(Disparo());
        }

       
    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(UnityEngine.Random.Range(-maxDistance, maxDistance), UnityEngine.Random.Range(-maxDistance, maxDistance));
    }

    private void Movement()
    {
        if (Deteccion)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
                
            }
            
        }
    }


    private void Detection()
    {
        if (PlayerInArea())
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


        dir = player.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        controlador.rotation = Quaternion.Euler(0, 0, angle);

        dir.Normalize();
    }

    IEnumerator Disparo()
    {
        
        Instantiate(balaenemigo, controlador.transform.position, controlador.transform.rotation);

        canAttack = false;

        yield return new WaitForSeconds(1);

        canAttack = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }


}
