using System;
using UnityEngine;

public class Hidrante : MonoBehaviour
{
    [SerializeField] float radio;
    [SerializeField] LayerMask capa;
    [SerializeField] LayerMask Activadores;
    [SerializeField] Player_Disparo disparo;
    [SerializeField] Animator animator;

    [NonSerialized] public bool broken;

    bool area()
    {
        return Physics2D.OverlapCircle(transform.position, radio, capa);
    }

    void Update()
    {
        animator.SetBool("Activo", broken); //Animacion

        //Recarga del arma secundaria
        if (area() == true && broken && disparo.BetaCurrentAmmo <= disparo.BetaMaxAmmo && !disparo.isShooting)
        {
            disparo.BetaCurrentAmmo += Time.deltaTime * 260;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);

            if(!broken) broken = true;
        }
    }
}

