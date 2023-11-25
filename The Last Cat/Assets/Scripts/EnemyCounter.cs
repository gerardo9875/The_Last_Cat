using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] int enemiesKilled;

    public bool AllEnemiesKilled;


    private void Update()
    {
        if(enemiesKilled >= enemies.Length)
        {
            AllEnemiesKilled = true;
        }
    }

    public void addEnemy()
    {
        enemiesKilled++;
    }
}
