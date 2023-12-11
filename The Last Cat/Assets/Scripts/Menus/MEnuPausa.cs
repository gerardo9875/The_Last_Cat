using UnityEngine;
using UnityEngine.SceneManagement;

public class MEnuPausa : MonoBehaviour
{
    public int actualscene;
    [SerializeField] private GameObject menuPausa;

    Player_Disparo shoot;
    Player_Life life;

    private bool juegoPausado=false;

    private void Awake()
    {
        life = GameObject.Find("Player").GetComponent<Player_Life>();
        shoot = GameObject.Find("Player").GetComponentInChildren<Player_Disparo>();
    }
    private void Update()
    { 
        if (menuPausa == null)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }
    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        menuPausa.SetActive(true);
    }
    public void Reanudar()
    {
        juegoPausado=false;
        Time.timeScale = 1f;

        menuPausa.SetActive(false);
    }
    public void Reiniciar()
    {
        juegoPausado = false;
        life.currentlife = life.maxLife;
        shoot.AlfaCurrentAmmo = shoot.AlfaMaxAmmo;
        shoot.BetaCurrentAmmo = shoot.BetaMaxAmmo;
        Time.timeScale = 1f;
        SceneManager.LoadScene(actualscene);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
