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
    [SerializeField] GameObject luzDisparo;
    public int AlfaMaxAmmo = 30;
    public int AlfaCurrentAmmo;
    public float reloadTime = 2.0f;
    private bool isReloading = false;
    private bool canShoot = true;

    [Header("Disparo Secundario")]
    [SerializeField] GameObject BetaBullet;
    public float BetaMaxAmmo = 350;
    public float BetaCurrentAmmo;

    [Header("Bomba Ratón")]
    [SerializeField] GameObject bombaRaton;
    [SerializeField] float RatonVel;
    public int ratonCount;

    [NonSerialized] public bool armaPrincipal = true;

    [Header("Verificar cuando dispara")]
    public bool isShooting;
    public float unshootingTime;
    public float passedTime;

    private void Start()
    {
        orientacion = GetComponent<Player_Orientacion>();
        AlfaCurrentAmmo = AlfaMaxAmmo;
        BetaCurrentAmmo = BetaMaxAmmo;
    }
    private void Update()
    {

        shootingBoolDelay();


        if (isReloading) return;


        ShootControllerVoid();


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

    void shootingBoolDelay()
    {
        if (isShooting && passedTime < unshootingTime)
        {
            passedTime += Time.deltaTime;
        }

        if (passedTime >= unshootingTime)
        {
            isShooting = false;
        }
    }

    void ShootControllerVoid()
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
                isShooting = true;
                passedTime = 0;

                Instantiate(AlfaBullet, transform.position, transform.rotation);
                AlfaCurrentAmmo--;

                canShoot = false;
                StartCoroutine(ShootLight());
                Invoke("ShootDelay", 0.18f);
            }
        }
    }


    void SecondaryShoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (BetaCurrentAmmo > 0)
            {
                isShooting = true;
                passedTime = 0;

                Instantiate(BetaBullet, transform.position, transform.rotation);
                BetaCurrentAmmo--;
            }
        }
    }


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
    }


    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        AlfaCurrentAmmo = AlfaMaxAmmo;
        isReloading = false;
    }


    /////////////////////////////////////////////////
    //VOIDS AUXILIARES PARA EL DISPARO PRINCIPAL
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
    /////////////////////////////////////////////////
}