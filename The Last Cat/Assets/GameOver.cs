using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;

    private bool muerte= false;
    public int actualscene;
    private void Update()
    {
        if (menuGameOver == null)
        {
            return;
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
