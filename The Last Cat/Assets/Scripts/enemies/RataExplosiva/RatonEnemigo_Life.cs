using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatonEnemigo_Life : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private Animator animator;

    RatonEnemigo mov;
    void Start()
    {
        animator = GetComponent<Animator>();
        mov = GetComponent<RatonEnemigo>();

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
                animator.Play("Exploding");
            }
        }
    }
}
