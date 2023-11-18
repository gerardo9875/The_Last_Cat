using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    Player_Movement mov;
    Player_Disparo shoot;

    public int maxLife;
    public int currentlife;
    public bool alive = true;

    public bool canRecieveDamage = true;
    private void Awake()
    {
        mov = GetComponent<Player_Movement>();
        shoot = GetComponentInChildren<Player_Disparo>();
    }

    private void Start()
    {
        currentlife = maxLife;
    }
    private void Update()
    {
        if (currentlife <= 0)
        {
            mov.canMove = false;
            shoot.canShoot = false;
            alive = false;
            canRecieveDamage = false;

            Rigidbody2D rgb = GetComponent<Rigidbody2D>();
            rgb.velocity = Vector3.zero;

            //StartCoroutine(ChangeScene());
        }

    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("GameOver1");
    }

    public IEnumerator RecieveDamage(int value)
    {
        if (alive)
        {
            canRecieveDamage = false;
            currentlife += value;

            yield return new WaitForSeconds(1);

            canRecieveDamage = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyDamageObj"))
        {

            if (collision.gameObject.GetComponent<Explosion_RataE>() != null)
            {
                if (canRecieveDamage)
                {
                    StartCoroutine(RecieveDamage(-2));

                    Vector2 dir = collision.gameObject.transform.position - transform.position;
                    mov.DamageFeedback(dir);
                }
            }
        }
    }
}
