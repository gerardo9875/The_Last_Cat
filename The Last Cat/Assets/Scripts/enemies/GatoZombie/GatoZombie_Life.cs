using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GatoZombie_Life : MonoBehaviour
{
    public float life;
    [NonSerialized] public bool alive = true;
    public bool toCounter = true;
    bool canAdd = true;

    Collider2D coll;
    GatoZombi_movement mov;
    EnemyCounter counter;

    private void Awake()
    {
        mov = GetComponent<GatoZombi_movement>();
        coll = GetComponent<Collider2D>();

        if (toCounter)
        {
            counter = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();
        }
    }

    private void Update()
    {
        if (life <= 0)
        {
            mov.canMove = false;
            mov.canShoot = false;
            mov.isShooting = false;
            mov.agent.SetDestination(transform.position);
            coll.enabled = false;

            alive = false;

            if (toCounter)
            {
                if (GameObject.Find("Estacion") != null && canAdd)
                {
                    EstationStop station = GameObject.Find("Estacion").GetComponent<EstationStop>();
                    station.enemyCounter++;
                    canAdd = false;
                }
                else if (canAdd)
                {
                    counter.addEnemy();
                    canAdd = false;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            life -= 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ExplBR"))
        {
            life -= 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterShoot"))
        {
            life -= Time.deltaTime;
        }
    }
    
}
