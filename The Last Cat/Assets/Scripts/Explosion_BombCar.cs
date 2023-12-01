using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Explosion_BombCar : MonoBehaviour
{
    float timer = 1.5f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0 ) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player_Life>() != null)
        {
            Player_Life life = collision.gameObject.GetComponent<Player_Life>();
            Player_Movement mov = collision.gameObject.GetComponent<Player_Movement>();

            if (life.canRecieveDamage)
            {
                Vector2 dir = transform.position - collision.gameObject.transform.position;
                mov.DamageFeedback(dir);
                StartCoroutine(life.RecieveDamage(-2));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Life>() != null)
        {
            Player_Life life = collision.gameObject.GetComponent<Player_Life>();
            life.canRecieveDamage = true;
        }

    }
}
