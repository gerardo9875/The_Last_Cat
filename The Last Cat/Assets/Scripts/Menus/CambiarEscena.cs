using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public static int CurrentLevel;

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject ComoJugarMenu;
    [SerializeField] GameObject SelectorDeNivelMenu;
    [SerializeField] Animator Fade;
    [SerializeField] GameObject Secuence;

    public void MainMenuStart()
    {
        Secuence.SetActive(true);
    }

    public void ChangeScene(string SceneName)
    {
        StartCoroutine(NextScene(SceneName));
    }

    public void QuitGame()
    {
        StartCoroutine(Salir());
    }

    IEnumerator NextScene(string SceneName)
    {
        Fade.Play("FadeOut");

        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene(SceneName);
    }
    IEnumerator Salir()
    {
        Fade.Play("FadeOut");

        yield return new WaitForSeconds(1.6f);

        Application.Quit();
    }

    public void ActiveComoJugar()
    {
        MainMenu.SetActive(false);
        ComoJugarMenu.SetActive(true);
        SelectorDeNivelMenu.SetActive(false);
    }

    public void ActiveSelectorDeNivel()
    {
        MainMenu.SetActive(false);
        ComoJugarMenu.SetActive(false);
        SelectorDeNivelMenu.SetActive(true);
    }

    public void ActiveMainMenu()
    {
        MainMenu.SetActive(true);
        ComoJugarMenu.SetActive(false);
        SelectorDeNivelMenu.SetActive(false);
    }

}
