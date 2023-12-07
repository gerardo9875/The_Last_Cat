using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BombaR : MonoBehaviour
{
    public int ammo;
    [SerializeField] ParticleSystem destroyParticles;
    Player_Disparo shootScript;
    private void Start()
    {
        shootScript = GameObject.Find("Player").GetComponentInChildren<Player_Disparo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && shootScript.ratonCount < 5)
        {
            shootScript.ratonCount += ammo;
            Instantiate(destroyParticles, transform.position, destroyParticles.transform.rotation);
            Destroy(gameObject);
        }
    }
}
