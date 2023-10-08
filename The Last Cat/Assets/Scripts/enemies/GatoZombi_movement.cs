using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoZombi_movement : MonoBehaviour
{
    Enemy_Detection detection;

    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] float minDistance;

    private void Awake()
    {
        detection = GetComponent<Enemy_Detection>();
    }

    private void Update()
    {
        if (detection.Deteccion)
        {
            if (Vector2.Distance(transform.position, player.position) < minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

        }
    }
}
