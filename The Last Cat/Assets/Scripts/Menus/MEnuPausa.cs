using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MEnuPausa : MonoBehaviour
{

    public int actualscene;
    [SerializeField] private GameObject menuPausa;


    private bool juegoPausado=false;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(actualscene);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
