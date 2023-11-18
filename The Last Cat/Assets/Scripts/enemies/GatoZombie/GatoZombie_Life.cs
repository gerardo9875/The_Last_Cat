using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GatoZombie_Life : MonoBehaviour
{
    [SerializeField] private float life;
    [NonSerialized] public bool alive = true;
    public bool toCounter = true;
    bool canAdd = true;

    Collider2D coll;
    GatoZombi_movement mov;

    private void Awake()
    {
        mov = GetComponent<GatoZombi_movement>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (life <= 0)
        {
            mov.canMove = false;
            mov.canShoot = false;
            mov.isShooting = false;
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
                else if (GameObject.Find("EnemyCounter") != null && canAdd)
                {
                    EnemyCounter counter = GameObject.Find("EnemyConuter").GetComponent<EnemyCounter>();
                    counter.addEnemy();
                    canAdd = false;

                }
            }

            Destroy(gameObject);
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
            life -= 1;
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
