using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarEscena : MonoBehaviour
{
    public Button button;
    public string sceneName;
    public static int CurrentLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(NextScene);
    }

    void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
