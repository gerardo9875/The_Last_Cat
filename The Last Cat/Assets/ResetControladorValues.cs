using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetControladorValues : MonoBehaviour
{
    [SerializeField] Controlador controlador;

    void Update()
    {
        controlador = GameObject.Find("Controlador").GetComponent<Controlador>();

        controlador.life = 7;
        controlador.maxlife = 7;
        controlador.muni1 = 30;
        controlador.muni2 = 360;
        controlador.bomba = 0;
    }
}
