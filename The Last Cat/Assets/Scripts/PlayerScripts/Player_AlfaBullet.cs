using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AlfaBullet : MonoBehaviour
{
    [SerializeField] ParticleSystem destroyingPartycles;
    [SerializeField] ParticleSystem enemyCollParticle;
    [SerializeField] float ShootVel;
    public double tiempo;
    private void Update()
    {
        //Direccion de la bala
        transform.Translate(Vector2.up * ShootVel * Time.deltaTime);

        //Destruir objeto
        tiempo -= Time.deltaTime;
        if (tiempo < 0) destroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            Invoke("destroyBullet", 0.01f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(enemyCollParticle, transform.position, enemyCollParticle.transform.rotation);
        }
    }

    void destroyBullet()
    {
        Instantiate(destroyingPartycles, transform.position, destroyingPartycles.transform.rotation);
        Destroy(gameObject);
    }
}
