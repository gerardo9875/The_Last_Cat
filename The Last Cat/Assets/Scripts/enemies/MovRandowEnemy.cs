using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRandowEnemy : MonoBehaviour
{
    [Header("Movimiento")]
    public float speedE;
    public float range;//Lo cerca que debe estar del punto inicial
    public float maxDistance;//Lejos puede llegar
   
    private Vector2 wayPoint;

    void Start()
    {
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {      
        transform.position = Vector2.MoveTowards(transform.position,wayPoint, speedE * Time.deltaTime);
        if (Vector2.Distance(transform.position,wayPoint) < range)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    
}
