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
        vida = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Life>();
        shoots = GameObject.FindGameObjectWithTag("ShootController").GetComponent<Player_Disparo>();
        life = vida.currentlife;
        muni1 = shoots.AlfaCurrentAmmo;
        muni2 = shoots.BetaCurrentAmmo;
    }
}
