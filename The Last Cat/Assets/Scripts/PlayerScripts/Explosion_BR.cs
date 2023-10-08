using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_BR : MonoBehaviour
{
    public float Contador = 3;
    // Update is called once per frame
    void Update()
    {
        Contador -= Time.deltaTime;
        if (Contador <= 0)
        {
            Destroy(gameObject);
        }
    }

}
