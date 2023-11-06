using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerControlado : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies1;
    [SerializeField] private GameObject[] enemies2;
    [SerializeField] private GameObject[] enemies3;
    [SerializeField] private GameObject[] enemies4; 
    [SerializeField] private GameObject[] enemies5;
    [SerializeField] private GameObject[] enemies6;
    [SerializeField] private GameObject[] enemies7;
    [SerializeField] private GameObject[] enemies8;

    public GameObject[] currentEnemies;

    List<GameObject[]> arrayLists = new List<GameObject[]>();

    int horda = 0;
    public bool canSpawn;
    public int spawnLimit;
    public int spawnCount;

    private void Start()
    {
        arrayLists.Add(enemies1);
        arrayLists.Add(enemies2);
        arrayLists.Add(enemies3);
        arrayLists.Add(enemies4);
        arrayLists.Add(enemies5);
        arrayLists.Add(enemies6);
        arrayLists.Add(enemies7);
        arrayLists.Add(enemies8);

        currentEnemies = arrayLists[horda];
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            for(int i  = 0; i < currentEnemies.Length; i++)
            {
                GameObject enemytoSpawn = currentEnemies[i];
                Instantiate(enemytoSpawn, transform.position, Quaternion.identity);

                spawnCount++;

                if (spawnCount >= currentEnemies.Length)
                {
                    canSpawn = false;
                    horda++;
                    currentEnemies = arrayLists[horda];
                    spawnCount = 0;
                }
            }
        }

    }
}
