using System.Collections;
using UnityEngine;

public class Hidrante : MonoBehaviour
{
    public float radio;
    public LayerMask capa;
    public Player_Disparo disparo;

    bool broken;

    bool area()
    {
        return Physics2D.OverlapCircle(transform.position, radio, capa);
    }

    void Update()
    {
        if (area() == true && broken && disparo.BetaCurrentAmmo <= disparo.BetaMaxAmmo)
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
            if(!broken) Destroy(collision.gameObject);

            broken = true;
        }
    }
}

