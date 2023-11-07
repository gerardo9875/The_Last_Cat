using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartTrainFinalLevel : MonoBehaviour
{


    private void Start()
    {
        StartCoroutine(activePlayer());
    }


    IEnumerator activePlayer()
    {
        yield return new WaitForSeconds(4.8f);

        Player_Disparo shoot = GameObject.Find("Player").GetComponentInChildren<Player_Disparo>();
        shoot.enabled = true;
    }
}
