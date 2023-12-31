using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] float ShootVel;
    [SerializeField] double tiempo;
    [SerializeField] ParticleSystem particulas;

    private void Update()
    {
        //Direccion de la bala
        transform.Translate(Vector2.up * ShootVel * Time.deltaTime);

        //Destruir objeto
        tiempo -= Time.deltaTime;
        if (tiempo < 0)
        {
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        Instantiate(particulas, transform.position, particulas.transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pared") || collision.CompareTag("Player"))
        {
            OnDestroy();
        }
    }
}
