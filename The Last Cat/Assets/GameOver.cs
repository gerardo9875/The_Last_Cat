using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    Player_Life life;
    private bool muerte= false;
    public int actualscene;
    private void Update()
    {
        life = GameObject.Find("Player").GetComponent<Player_Life>();

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

       muerte = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(actualscene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
