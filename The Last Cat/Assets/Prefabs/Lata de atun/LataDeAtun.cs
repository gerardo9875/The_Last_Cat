using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LataDeAtun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Life life = collision.gameObject.GetComponent<Player_Life>();

            if(life.currentlife < life.maxLife)
            {
                life.currentlife = life.maxLife;
                life.vidaUI.Life(life.currentlife);
                Destroy(gameObject);
            }
        }
    }


}
