using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Disparo : MonoBehaviour
{
    Player_Orientacion orientacion;

    [Header("Disparo Principal")]
    [SerializeField] GameObject AlfaBullet;
    public int AlfaMaxAmmo = 30;
    public int AlfaCurrentAmmo;
    public float reloadTime = 2.0f;
    public bool isReloading = false;
    bool canShoot = true;
    [SerializeField] GameObject luzDisparo;

    [Header("Disparo Secundario")]
    [SerializeField] GameObject BetaBullet;
    public float BetaMaxAmmo = 350;
    public float BetaCurrentAmmo;

    [Header("Bomba Ratón")]
    [SerializeField] GameObject bombaRaton;
    public int ratonCount;
    [SerializeField] float RatonVel;

    [NonSerialized] public bool armaPrincipal = true; //Posible variable para usar en el codigo del HUD

    private void Start()
    {
        orientacion = GetComponent<Player_Orientacion>();
        AlfaCurrentAmmo = AlfaMaxAmmo;
        BetaCurrentAmmo = BetaMaxAmmo;
    }
    private void Update()
    {
        if (isReloading)
            return;

        ShootVoid();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            armaPrincipal = !armaPrincipal;
        }

        //Recarga manual y automatica
        if (Input.GetKeyDown(KeyCode.R) || AlfaCurrentAmmo == 0)
        {
            StartCoroutine(Reload());
        }
    }
    public void ShootVoid()
    {
        switch (armaPrincipal) //Switch para saber que arma esta seleccionada
        {
            case true:
                {
                    PrincipalShoot();
                    break;
                }
            case false:
                {
                    SecondaryShoot();
                    break;
                }
        }

        BombaRatonVoid();
    }

    void PrincipalShoot()
    {
        //Si dispara o no esta recargando
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (AlfaCurrentAmmo > 0 && canShoot) //Si la municion es mayor a 0
            {
                Instantiate(AlfaBullet, transform.position, transform.rotation);
                AlfaCurrentAmmo--;

                canShoot = false;
                StartCoroutine(ShootLight());
                Invoke("ShootDelay", 0.18f);
            }
        }
    }   //Disparo del arma Alfa
    IEnumerator ShootLight()
    {
        luzDisparo.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        luzDisparo.SetActive(false);
    }
    void ShootDelay()
    {
        canShoot = true;
    }
    void SecondaryShoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (BetaCurrentAmmo > 0)
            {
                Instantiate(BetaBullet, transform.position, transform.rotation);
                BetaCurrentAmmo--;
            }
        }
    }   //Disparo del arma Beta
    void BombaRatonVoid()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (ratonCount > 0)
            {
                GameObject bomba = Instantiate(bombaRaton, transform.position, transform.rotation);
                ratonCount--;
                Rigidbody2D bombaRb = bomba.GetComponent<Rigidbody2D>();
                bombaRb.velocity = new Vector2(orientacion.direccion.normalized.x, orientacion.direccion.normalized.y) * RatonVel * Time.deltaTime * 10;
            }
        }
    }   //Disparo de bomba raton
    public IEnumerator Reload()     //Void de recarga
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        AlfaCurrentAmmo = AlfaMaxAmmo;
        isReloading = false;
    }
}
