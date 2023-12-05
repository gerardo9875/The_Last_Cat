using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDialogo : MonoBehaviour
{
   

    [SerializeField] bool unicaInteraccion;
    [SerializeField] UnityEvent OnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnTrigger.Invoke();
            if (unicaInteraccion) Destroy(this);
        }
    }
}
