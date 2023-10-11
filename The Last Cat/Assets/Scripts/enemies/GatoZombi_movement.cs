using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoZombi_movement : MonoBehaviour
{
    [Header("Deteccion")]
    [SerializeField] float radio;
    [SerializeField] float timeOut;
    public LayerMask PlayerLayer;
    private float CurrentTime;
    private bool Deteccion;

    [Header("Movimiento")]
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float minDistance;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, radio, PlayerLayer);
    }


    private void Start()
    {
        CurrentTime = timeOut;
    }


    private void Update()
    {
        Movement();
        Detection();
    }


    private void Movement()
    {
        if (Deteccion)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            }
            else
            {
                //Disparo
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }


}
