using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Armas_UI : MonoBehaviour
{
    [SerializeField] Player_Disparo disparoScript;

    [Header("Arma Seleccionada")]
    [SerializeField] Image imagenPrincipal;
    [SerializeField] Image imagenSecondary;
    [SerializeField] GameObject principalAmmoCounter;
    [SerializeField] GameObject secondaryAmmoCounter;

    [Header("Municion Arma Principal")]
    [SerializeField] TextMeshProUGUI text;

    [Header("Municion Arma Secundaria")]
    [SerializeField] Image BetaMunUI;

    private void Update()
    {

        SelectedGun();
        PrincipalAmmo();
        SecondaryAmmo();

    }


    private void SelectedGun()
    {
        if (disparoScript.armaPrincipal)
        {
            //Municion actual
            principalAmmoCounter.SetActive(true);
            secondaryAmmoCounter.SetActive(false);
            //Arma Actual
            imagenPrincipal.color = Color.white;
            imagenSecondary.color = Color.gray;
        }
        else
        {
            //Municion actual
            principalAmmoCounter.SetActive(false);
            secondaryAmmoCounter.SetActive(true);
            //Arma Actual
            imagenPrincipal.color = Color.gray;
            imagenSecondary.color = Color.white;
        }
    }


    private void PrincipalAmmo()
    {
        int currentAmmo = disparoScript.AlfaCurrentAmmo;
        int maxAmmo = disparoScript.AlfaMaxAmmo;

        string format = "{00}/{1}";
        text.SetText(format, currentAmmo, maxAmmo);
    }
    private void SecondaryAmmo()
    {
        BetaMunUI.fillAmount = disparoScript.BetaCurrentAmmo / disparoScript.BetaMaxAmmo;
    }


}
