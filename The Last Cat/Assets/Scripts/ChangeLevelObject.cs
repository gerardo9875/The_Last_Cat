using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelObject : MonoBehaviour
{
    public string nameLevel;

    private void Start()
    {
        SceneManager.LoadScene(nameLevel);
    }
}
