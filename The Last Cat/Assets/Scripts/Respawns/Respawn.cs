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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (canSpawn)
        {
            spawnLimit = Random.Range(6, 12);

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

    private IEnumerator Spawner()
    {
        spawnLimit = Random.Range(6, 12);

        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn) 
        {
            yield return wait;
            int rand = Random.Range(0, enemies.Length);

            GameObject  enemytoSpawn = enemies[rand];
            Instantiate(enemytoSpawn,transform.position,Quaternion.identity);

            spawnCount++;
            

            

        }
    }
}
