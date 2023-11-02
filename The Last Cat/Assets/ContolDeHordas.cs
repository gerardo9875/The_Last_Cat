using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContolDeHordas : MonoBehaviour
{
    [SerializeField] EstationStop station;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !station.Arrive)
        {
            station.Arrive = true;
        }
    }
}
