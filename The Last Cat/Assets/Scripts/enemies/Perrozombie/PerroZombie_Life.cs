using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroZombie_Life : MonoBehaviour
{
    [SerializeField] private int life;
    [NonSerialized] public bool alive = true;
    public bool toCounter = true;

    PerroZombi_movement mov;
    EstationStop station;
    void Start()
    {

        mov = GetComponent < PerroZombi_movement>();
        
        if(toCounter )
        {
            if(GameObject.Find("Estacion") != null)
            {
                station = GameObject.Find("Estacion").GetComponent<EstationStop>();
            }
            else
            {
                //Codigo del otro contador de enemigos
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            life--;
            Destroy(collision.gameObject);

            if (life <= 0)
            {
                mov.canMove = false;
                alive = false;

                if(toCounter) 
                {
                    station.enemyCounter++;
                }

                Destroy(gameObject);
            }
        }
    }
}
