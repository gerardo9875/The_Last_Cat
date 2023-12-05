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
    AudioSource damage;
    Controlador control;

    [NonSerialized] public Vida vidaUI;

    public int maxLife;
    public int currentlife;
    public bool alive = true;

    public bool canRecieveDamage = true;
    public bool addExtraLife = true;
    private void Awake()
    {
        if(control != null){
            if (GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>())
            {
                control = GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>();
                currentlife = control.life;
            }
        }

        else
        {
            currentlife = maxLife;
        }
        
        
        mov = GetComponent<Player_Movement>();
        shoot = GetComponentInChildren<Player_Disparo>();
        damage = GetComponent<AudioSource>();
    }

    
    private void Update()
    {
        if (vidaUI == null && GameObject.Find("HUD")) vidaUI = GameObject.Find("HUD").GetComponentInChildren<Vida>();

        if (currentlife <= 0)
        {
            mov.canMove = false;
            shoot.canShoot = false;
            alive = false;
            canRecieveDamage = false;

            Rigidbody2D rgb = GetComponent<Rigidbody2D>();
            rgb.velocity = Vector3.zero;

            StartCoroutine(ChangeScene());
        }

        if (maxLife == 8 && addExtraLife)
        {
            vidaUI.NewLIfe();
            addExtraLife = false;
        }
        else if (maxLife == 9)
        {
            vidaUI.NewLIfe();
            vidaUI.NewLIfe();
            addExtraLife = false;
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

            vidaUI.Life(currentlife);

            yield return new WaitForSeconds(1);

            canRecieveDamage = true;
            damage.Play();

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

            if (collision.gameObject.GetComponent<BalaEnemigo>() != null)
            {
                if (canRecieveDamage)
                {
                    StartCoroutine(RecieveDamage(-1));

                    Vector2 dir = collision.gameObject.transform.position - transform.position;
                    mov.DamageFeedback(dir);
                }

                Destroy(collision.gameObject);
            }
        }
    }
}
