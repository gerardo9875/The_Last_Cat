using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bombaRaton : MonoBehaviour
{
    [SerializeField] float Contador;
    [SerializeField] GameObject Explosion;

    void Update()
    {
        Contador -= Time.deltaTime;
        if (Contador <= 0)
        {
            AutoDestroy();
        }
    }
    void AutoDestroy()
    {
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
