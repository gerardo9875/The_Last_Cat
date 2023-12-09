using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    Player_Life life;
    public int actualscene;

    Player_Disparo shoot;
    private void Update()
    {
        life = GameObject.Find("Player").GetComponent<Player_Life>();
        shoot = GameObject.Find("Player").GetComponentInChildren<Player_Disparo>();

        if (menuGameOver == null)
        {
            return;
        }

        if(life.currentlife == 0)
        {
            menuGameOver.SetActive(true);
        }
    }

    public void Reiniciar()
    {
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
