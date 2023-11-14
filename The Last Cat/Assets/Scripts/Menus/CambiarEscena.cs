using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarEscena : MonoBehaviour
{
    public static int CurrentLevel;

    [SerializeField] GameObject Fade;
    [SerializeField] GameObject Secuence;

    public void MainMenuStart()
    {
        Secuence.SetActive(true);
    }

    public void ChangeScene(string SceneName)
    {
        StartCoroutine(NextScene(SceneName));
    }

    IEnumerator NextScene(string SceneName)
    {
        Fade.SetActive(true);
        Animator anim = Fade.GetComponent<Animator>();
        anim.Play("FadeOut");

        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene(SceneName);
    }
}
