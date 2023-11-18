using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion_BR : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hidrante>() != null)
        {
            Hidrante h = collision.gameObject.GetComponent<Hidrante>();
            h.broken = true;
        }
    }


}
