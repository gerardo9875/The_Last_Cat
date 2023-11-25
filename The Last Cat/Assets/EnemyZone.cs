using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyZone : MonoBehaviour
{
    bool PlayerInArea = false;
    public CinemachineVirtualCamera camara;

    private void Update()
    {
        if (PlayerInArea)
        {
            camara.Priority = 11;
        }
        else
        {
            camara.Priority = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInArea = false;
    }
}
