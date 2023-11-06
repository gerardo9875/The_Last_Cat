using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PerroZombie_Life : MonoBehaviour
{
    [SerializeField] private float life;
    public bool alive = true;
    public bool toCounter = true;

    Collider2D coll;
    PerroZombi_movement mov;

    void Start()
    {

        mov = GetComponent < PerroZombi_movement>();
        coll = GetComponent < Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            mov.canMove = false;
            mov.isAttacking = true;
            mov.canDoDamage = false;
            mov.canRotate = false;
            coll.enabled = false;

            alive = false;

            if (toCounter)
            {
                if (GameObject.Find("Estacion") != null)
                {
                    EstationStop station = GameObject.Find("Estacion").GetComponent<EstationStop>();
                    station.enemyCounter++;
                }
                else if (GameObject.Find("EnemyCounter") != null)
                {
                    EnemyCounter counter = GameObject.Find("EnemyConuter").GetComponent<EnemyCounter>();
                    counter.addEnemy();
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterShoot"))
        {
            life -= Time.deltaTime;
        }
    }
}
