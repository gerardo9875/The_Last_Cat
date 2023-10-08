using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    int maxLife = 7;
    int currentlife;

    private void Start()
    {
        currentlife = 1;

        if(currentlife<=0)
        {
            SceneManager.LoadScene("GameOver1");
        }

    }
}
