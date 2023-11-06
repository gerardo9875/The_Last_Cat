using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] float ShootVel;
    [SerializeField] double tiempo;
    private void Update()
    {
        //Direccion de la bala
        transform.Translate(Vector2.up * ShootVel * Time.deltaTime);

        //Destruir objeto
        tiempo -= Time.deltaTime;
        if (tiempo < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Life life = collision.gameObject.GetComponent<Player_Life>();
            life.currentlife--;

            Destroy(gameObject);
        }
    }
}
