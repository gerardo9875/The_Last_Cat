using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LoboZombie_AI : MonoBehaviour
{
    [Header("Player Detection")]
    [SerializeField] Transform[] detectionDistance;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask playerLayer;
    [NonSerialized] public bool Deteccion;
    [SerializeField] float visionTimer;
    float timer;
    int currentPos;

    [Header("Movimiento")]
    [SerializeField] float speed;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.angularSpeed = 1000;
        agent.acceleration = 1000;
        agent.speed = speed;
    }

    bool playerInSight()
    {
        return Physics2D.Linecast(transform.position, detectionDistance[currentPos].position);
    }
    private void Update()
    {

        Detection();
        Attack();

    }

    private void Attack()
    {
        if(Deteccion)
        {
            agent.SetDestination(player.transform.position);

            Player_Life life = player.GetComponent<Player_Life>();
            life.currentlife -= life.currentlife;

            if(Vector2.Distance(transform.position, player.transform.position) <= 0)
            {
                agent.SetDestination(transform.position);
            }
        }   
    }

    private void Detection()
    {
        if (playerInSight())
        {
            Deteccion = true;
        }


        //Cambiar Posicion del raycast
        if (timer < visionTimer)
        {
            timer += Time.deltaTime;
        }

        if (timer > visionTimer)
        {
            if (!Deteccion)
            {
                currentPos++;
                timer = 0;

                if (currentPos >= detectionDistance.Length)
                {
                    currentPos = 0;
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, detectionDistance[currentPos].position);
    }
}
