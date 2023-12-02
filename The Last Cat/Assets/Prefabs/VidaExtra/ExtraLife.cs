using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] ParticleSystem destroyParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vida vidaUI = GameObject.Find("HUD").GetComponentInChildren<Vida>();
            vidaUI.NewLIfe();

            Player_Life life = GameObject.Find("Player").GetComponent<Player_Life>();
            life.maxLife++;
            life.currentlife = life.maxLife;

            Instantiate(destroyParticles, transform.position, destroyParticles.transform.rotation);

            Destroy(gameObject);
        }
    }
}
