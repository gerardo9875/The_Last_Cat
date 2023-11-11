using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Disparo : MonoBehaviour
{
    Player_Orientacion orientacion;
    Player_Movement mov;

    [Header("Disparo Principal")]
    [SerializeField] public GameObject luzDisparo;
    [SerializeField] GameObject AlfaBullet;
    [SerializeField] Transform shootPoint;
    [NonSerialized] public bool canShoot = true;
    public int AlfaMaxAmmo = 30;
    public int AlfaCurrentAmmo;
    public float reloadTime = 2.0f;
    private bool isReloading = false;

    [Header("Disparo Secundario")]
    [SerializeField] GameObject BetaBullet;
    public float BetaMaxAmmo = 350;
    public float BetaCurrentAmmo;
    public bool betaShoting;

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
        mov = GetComponentInParent<Player_Movement>();
        AlfaCurrentAmmo = AlfaMaxAmmo;
        BetaCurrentAmmo = BetaMaxAmmo;

        BetaBullet.SetActive(false);
    }
    private void Update()
    {

        shootingBoolDelay();

        if (mov.isdashing) canShoot = false;

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

                Instantiate(AlfaBullet, shootPoint.position, transform.rotation);
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

                betaShoting = true;
                BetaCurrentAmmo--;
            }
            else
            {
                betaShoting = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            betaShoting = false;
        }

        if(betaShoting) BetaBullet.SetActive(true);
        else BetaBullet.SetActive(false);
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
                orientacion.direccion.Normalize();
                bombaRb.velocity = new Vector2(orientacion.direccion.x, orientacion.direccion.y) * RatonVel * Time.fixedDeltaTime * 10;
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