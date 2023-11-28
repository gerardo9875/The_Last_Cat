using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    [NonSerialized] public bool AllEnemiesKilled;
    public int enemiesKilled;


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
