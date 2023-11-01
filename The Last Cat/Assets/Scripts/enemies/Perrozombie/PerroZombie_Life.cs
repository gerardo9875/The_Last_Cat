using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroZombie_Life : MonoBehaviour
{
    [SerializeField] private int life;
    

    PerroZombi_movement mov;
    void Start()
    {

        mov = GetComponent < PerroZombi_movement>();

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
                Destroy(gameObject);
            }
        }
    }
}
