using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmaActuai_UI : MonoBehaviour
{
    [SerializeField] Player_Disparo disparoScript;

    [Header("Arma Seleccionada")]
    [SerializeField] Image Principal;
    [SerializeField] Image Secondary;

    [Header("Municion")]
    [SerializeField] GameObject principalAmmoCounter;
    [SerializeField] GameObject secondaryAmmoCounter;

    [Header("Beta Municion UI")]
    [SerializeField] Image BetaMunUI;

    Color white = Color.white;
    Color gray = Color.gray;

    private void Update()
    {
        if (disparoScript.armaPrincipal)
        {
            //Municion actual
            principalAmmoCounter.SetActive(true);
            secondaryAmmoCounter.SetActive(false);
            //Arma Actual
            Principal.color = white;
            Secondary.color = gray;
        }
        else
        {
            //Municion actual
            principalAmmoCounter.SetActive(false);
            secondaryAmmoCounter.SetActive(true);
            //Arma Actual
            Principal.color = gray;
            Secondary.color = white;
        }

        //Municion beta UI
        BetaMunUI.fillAmount = disparoScript.BetaCurrentAmmo / disparoScript.BetaMaxAmmo;
    }
}
