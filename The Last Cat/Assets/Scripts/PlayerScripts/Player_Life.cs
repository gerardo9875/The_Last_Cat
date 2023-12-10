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
    Controlador control;

    AudioSource audioSource;
    [SerializeField] AudioClip Daño;

    [NonSerialized] public Vida vidaUI;

    public int maxLife;
    public int currentlife;
    public bool alive = true;

    public bool canRecieveDamage = true;
    public bool addExtraLife = true;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Controlador"))
        {
            control = GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>();
            maxLife = control.maxlife;
            currentlife = control.life;
        }
        else
        {
            currentlife = maxLife;
        }

        
        
        mov = GetComponent<Player_Movement>();
        shoot = GetComponentInChildren<Player_Disparo>();
        audioSource = GetComponent<AudioSource>();
    }

    
    private void Update()
    {
        if (vidaUI == null)
        {
            if (GameObject.Find("HUD"))
            {
                vidaUI = GameObject.Find("HUD").GetComponentInChildren<Vida>();
            }
        }
        else
        {
            for (int i = 0; i < vidaUI.vidas.Length; i++)
            {
                if (!vidaUI.vidas[i].activeInHierarchy == true)
                {
                    vidaUI.Life(currentlife);
                }

            }
        }

        if (maxLife == 8 && addExtraLife && vidaUI != null)
        {
            vidaUI.NewLIfe();
            addExtraLife = false;
        }
        else if (maxLife == 9 && vidaUI != null)
        {
            vidaUI.NewLIfe();
            vidaUI.NewLIfe();
            addExtraLife = false;
        }

        if (currentlife <= 0)
        {
            mov.canMove = false;
            shoot.canShoot = false;
            alive = false;
            canRecieveDamage = false;

            Rigidbody2D rgb = GetComponent<Rigidbody2D>();
            rgb.velocity = Vector3.zero;
        }

    }

    public IEnumerator RecieveDamage(int value)
    {
        if (alive)
        {
            audioSource.PlayOneShot(Daño);

            canRecieveDamage = false;
            currentlife += value;

            vidaUI.Life(currentlife);

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
