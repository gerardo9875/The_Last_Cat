using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_RataE : MonoBehaviour
{
    public void destroyObject() //Evento para destruir el objeto cuando termine la animacion
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        //logica de daño

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player_Life>() != null)
        {
            Player_Life player_Life = collision.gameObject.GetComponent<Player_Life>();

            player_Life.currentlife--;
        }
    }
}
