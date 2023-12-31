using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmaActuai_UI : MonoBehaviour
{
    [SerializeField] Player_Disparo disparoScript;

    [Header("Arma Seleccionada")]
    [SerializeField] Transform[] posiciones;
    [SerializeField] Image Principal;
    [SerializeField] Image Secondary;

    [Header("Municion")]
    [SerializeField] GameObject[] principalAmmo;
    TextMeshProUGUI MunicionTex;
    public TextMeshProUGUI BombaTex;

    //Barra de recarga
    [SerializeField] Image reloadBar;
    float startTime;

    [SerializeField] GameObject secondaryAmmo;

    Color white = Color.white;
    Color gray = Color.gray;


    private void Start()
    {
        if(disparoScript == null ) disparoScript = GameObject.Find("Player").GetComponentInChildren<Player_Disparo>();

        MunicionTex = principalAmmo[0].GetComponent<TextMeshProUGUI>();

        startTime = Time.time;

    }
    private void Update()
    {
        if (disparoScript.armaPrincipal)
        {
            //Municion actual
            principalAmmo[0].SetActive(true);
            principalAmmo[1].SetActive(true);
            secondaryAmmo.SetActive(false);

            //Arma Actual
            Principal.color = Color.white;
            Secondary.color = Color.gray;

            Principal.transform.position = posiciones[0].position;
            Principal.transform.localScale = posiciones[0].localScale;

            Secondary.transform.position = posiciones[1].position;
            Secondary.transform.localScale = posiciones[1].localScale;
        }
        else
        {
            //Municion actual
            principalAmmo[0].SetActive(false);
            principalAmmo[1].SetActive(false);
            secondaryAmmo.SetActive(true);
            //Arma Actual
            Principal.color = gray;
            Secondary.color = white;

            Secondary.transform.position = posiciones[0].position;
            Secondary.transform.localScale = posiciones[0].localScale;

            Principal.transform.position = posiciones[1].position;
            Principal.transform.localScale = posiciones[1].localScale;
        }

        //Municion alfa UI
        int currentAmmo = disparoScript.AlfaCurrentAmmo; 
        int maxAmmo = disparoScript.AlfaMaxAmmo;

        string format = "{00}/{1}";
        MunicionTex.SetText(format, currentAmmo, maxAmmo);

        int ratonCount = disparoScript.ratonCount;
        

        string Formato = "x{0}";
       BombaTex.SetText(Formato, ratonCount);

        //Indicador de recarga
        if (disparoScript.isReloading)
        {
            reloadBar.fillAmount = 0;

            float progreso = (Time.time - startTime) / disparoScript.reloadTime;
            progreso = Mathf.Clamp01(progreso);

            reloadBar.fillAmount = progreso;

            if(progreso >= 1)
            {
                startTime = Time.time;
            }

        }
        else
        {
            reloadBar.fillAmount = 0;
        }


        //Municion beta UI
        Image imagen = secondaryAmmo.GetComponent<Image>();

        imagen.fillAmount = disparoScript.BetaCurrentAmmo / disparoScript.BetaMaxAmmo;
    }
}
