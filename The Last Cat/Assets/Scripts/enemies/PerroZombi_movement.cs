using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PerroZombi_movement : MonoBehaviour
{
    Enemy_Detection detection;

    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] float minDistance;

    bool canMove = true;

    private void Awake()
    {
        detection = GetComponent<Enemy_Detection>();
    }

    private void Update()
    {
        if (detection.Deteccion && canMove)
        {
            if (Vector2.Distance(transform.position, player.position) > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                //ataque
            }
        }
    }
}
