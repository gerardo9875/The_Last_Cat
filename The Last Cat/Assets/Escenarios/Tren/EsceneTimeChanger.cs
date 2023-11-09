using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EsceneTimeChanger : MonoBehaviour
{
    float time;
    public string SceneToLoad;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 7f)
        {
            SceneManager.LoadScene(SceneToLoad);

        }
    }
}
