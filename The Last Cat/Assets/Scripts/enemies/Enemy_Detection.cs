using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Detection : MonoBehaviour
{
    [Header("Deteccion")]
    [SerializeField] float radio;
    [SerializeField] float timeOut;
    float CurrentTime;
    public bool Deteccion;
    [SerializeField] LayerMask PlayerLayer;

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
        if (PlayerInArea())
        {
            Deteccion = true;
            CurrentTime = timeOut;
        }
        else
        {
            CurrentTime -= Time.deltaTime;
        }

        if(CurrentTime < 0 && Deteccion)
        {
            Deteccion=false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
