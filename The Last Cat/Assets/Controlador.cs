using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public static Controlador gamestate;
    public int life, muni1, bomba;
    public float muni2;
    public Player_Life vida;
    public Player_Disparo shoots;

    private void Awake()
    {

        if (gamestate == null)
        {
            gamestate = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (gamestate != this)
        {
            Destroy(gameObject);
        }


    }

    private void Update()
    {
        if(vida == null)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Life>())
            {
                vida = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Life>();
            }
        }
        else
        {
            life = vida.currentlife;

        }

        if(shoots == null)
        {
            Debug.Log("1");
            if (GameObject.FindGameObjectWithTag("ShootController").GetComponent<Player_Disparo>())
            {
                Debug.Log("2");
                shoots = GameObject.FindGameObjectWithTag("ShootController").GetComponent<Player_Disparo>();
            }

        }
        else
        {
            muni1 = shoots.AlfaCurrentAmmo;
            muni2 = shoots.BetaCurrentAmmo;
            bomba = shoots.ratonCount;
        }
    }
}
