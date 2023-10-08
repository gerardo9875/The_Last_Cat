using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    [SerializeField] int maxLife = 7;
    public int currentlife;

    private void Start()
    {
        currentlife = maxLife;
    }
    private void Update()
    {
        if (currentlife <= 0)
        {
            SceneManager.LoadScene("GameOver1");
        }
    }
}
