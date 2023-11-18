using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BombaR : MonoBehaviour
{
    public int ammo;
    Player_Disparo shootScript;
    private void Start()
    {
        shootScript = GameObject.Find("Player").GetComponentInChildren<Player_Disparo>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shootScript.ratonCount += 1;
            Destroy(gameObject);
        }
    }
}
