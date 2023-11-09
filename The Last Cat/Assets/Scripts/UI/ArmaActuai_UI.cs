using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmaActuai_UI : MonoBehaviour
{
    Player_Disparo disparoScript;
    Player_Life lifeScript;

    [Header("Vidas")]
    [SerializeField] TextMeshProUGUI textVidas;

    [Header("Arma Seleccionada")]
    [SerializeField] Image Principal;
    [SerializeField] Image Secondary;

    [Header("Municion")]
    [SerializeField] GameObject principalAmmo;
    TextMeshProUGUI text; 

    [SerializeField] GameObject secondaryAmmo;

    Color white = Color.white;
    Color gray = Color.gray;


    private void Start()
    {
        text = principalAmmo.GetComponent<TextMeshProUGUI>();
        disparoScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Disparo>();
        lifeScript = GameObject.Find("Player").GetComponent<Player_Life>();
    }
    private void Update()
    {
        if (disparoScript.armaPrincipal)
        {
            //Municion actual
            principalAmmo.SetActive(true);
            secondaryAmmo.SetActive(false);
            //Arma Actual
            Principal.color = Color.white;
            Secondary.color = Color.gray;
        }
        else
        {
            //Municion actual
            principalAmmo.SetActive(false);
            secondaryAmmo.SetActive(true);
            //Arma Actual
            Principal.color = gray;
            Secondary.color = white;
        }

        //Vidas UI
        int maxLife = lifeScript.maxLife;
        int currentLife = lifeScript.currentlife;

        string lifeformat = "{0}/{1}";
        textVidas.SetText(lifeformat, currentLife, maxLife);


        //Municion alfa UI
        int currentAmmo = disparoScript.AlfaCurrentAmmo;
        int maxAmmo = disparoScript.AlfaMaxAmmo;

        string format = "{00}/{1}";
        text.SetText(format, currentAmmo, maxAmmo);


        //Municion beta UI
        Image imagen = secondaryAmmo.GetComponent<Image>();

        imagen.fillAmount = disparoScript.BetaCurrentAmmo / disparoScript.BetaMaxAmmo;
    }
}
