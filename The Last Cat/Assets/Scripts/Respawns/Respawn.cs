using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject[] enemies;

    public bool canSpawn = true;
    public int spawnLimit;
    public int spawnCount;

    public bool randomEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(randomEnemies)
        {
            instatiateRandom();
        }
        else
        {

        }
    }

    void instatiateRandom()
    {
        while (canSpawn)
        {
            spawnLimit = Random.Range(6, 9);

            int rand = Random.Range(0, enemies.Length);

            GameObject enemytoSpawn = enemies[rand];
            Instantiate(enemytoSpawn, transform.position, Quaternion.identity);

            spawnCount++;

            if (spawnCount >= spawnLimit)
            {
                canSpawn = false;
            }
        }
    }
}
