using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    int enemiesKilled;

    [NonSerialized] public bool AllEnemiesKilled;


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
