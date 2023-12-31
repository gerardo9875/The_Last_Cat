using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PerroZombie_Life : MonoBehaviour
{
    public float life;
    public bool alive = true;
    public bool toCounter = true;

    [Header("Sonidos")]
    [SerializeField] AudioClip deathSound;
    bool canSound = true;

    Collider2D coll;
    PerroZombi_movement mov;
    EnemyCounter counter;

    bool canAdd = true;

    void Start()
    {
        mov = GetComponent < PerroZombi_movement>();
        coll = GetComponent < Collider2D>();

        if (toCounter && GameObject.Find("EnemyCounter"))
        {
            counter = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();
        }

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

            mov.lastDir = mov.dir;

            if (canSound)
            {
                mov.AttackSound = null;
                mov.audioSourse.PlayOneShot(deathSound);
                canSound = false;
            }

            alive = false;

            if (toCounter)
            {
                if (GameObject.Find("Estacion") != null && canAdd)
                {
                    EstationStop station = GameObject.Find("Estacion").GetComponent<EstationStop>();
                    station.enemyCounter++;
                    canAdd = false;
                }
                else if (canAdd && counter!= null)
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
            life -= 5;
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
