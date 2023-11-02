using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_RataE : MonoBehaviour
{
    Player_Life player_Life;

    bool canDoDamage;

    //private void Start()
    //{
    //    player_Life = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Life>();
    //}

    public void destroyObject() //Evento para destruir el objeto cuando termine la animacion
    {
        Destroy(gameObject);
    }

    //private void Update()
    //{
         
    //    if(canDoDamage)
    //    {
    //        canDoDamage = false;
    //        player_Life.currentlife--;
    //    }


    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.GetComponent<Player_Life>() != null)
    //    {
    //        canDoDamage = true;
            
    //    }
    //}
}
