using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class EstationStop : MonoBehaviour
{
    public bool Random;

    [SerializeField] Respawn spawner;
    [SerializeField] SpawnerControlado spawner2;
    [SerializeField] DoorsController controller;
    Rigidbody2D rgb;


    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin noiseProfile;

    public int enemyCounter;
    public bool Arrive;
    public float speed;
    public float waitTime;
    bool canArrive = true;


    private void Awake()
    {

        if(Random)
        {
            spawner = GameObject.Find("Spawner").GetComponent<Respawn>();
        }
        else
        {
            spawner2 = GameObject.Find("Spawner").GetComponent<SpawnerControlado>();
        }

        rgb = GetComponent<Rigidbody2D>();

        noiseProfile = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noiseProfile.m_AmplitudeGain = 0.25f;

        StartCoroutine(Arriving());
    }

    private void Update()
    {
        if (Random)
        {
            if (enemyCounter >= spawner.spawnCount)
            {
                StartCoroutine(Arriving());
            }

            if (Arrive)
            {
                if (canArrive)
                {
                    enemyCounter = 0;
                    spawner.spawnCount = 0;

                    if (transform.position.x <= 0)
                    {
                        rgb.velocity = new Vector2(speed, 0);
                    }
                    else
                    {
                        noiseProfile.m_AmplitudeGain = 0f;
                        StartCoroutine(EnemiesLandingTime());
                    }

                }
            }

            if (transform.position.x >= 100)
            {
                canArrive = true;

                Vector2 pos = transform.position;
                pos.x = -100;
                transform.position = pos;

                rgb.velocity = new Vector2(0, 0);
            }
        }
        else //Cuando no es aleatorio
        {
            if(enemyCounter >= spawner2.currentEnemies.Length && spawner2.horda < spawner2.arrayLists.Count)
            {
                StartCoroutine (Arriving());
            }

            if (Arrive)
            {
                if (canArrive)
                {
                    enemyCounter = 0;

                    if (transform.position.x <= 0)
                    {
                        rgb.velocity = new Vector2(speed, 0);
                    }
                    else
                    {
                        noiseProfile.m_AmplitudeGain = 0f;
                        StartCoroutine(EnemiesLandingTime());
                    }
                }
            }

            if (transform.position.x >= 100)
            {
                canArrive = true;

                Vector2 pos = transform.position;
                pos.x = -100;
                transform.position = pos;

                rgb.velocity = new Vector2(0, 0);
            }
        }
        
    }

    IEnumerator EnemiesLandingTime()
    {
        controller.open = true;
        canArrive = false;

        rgb.velocity = new Vector2(0, 0);

        if (Random)
        {
            spawner.canSpawn = true;
        }
        else
        {
            spawner2.canSpawn = true;
        }


        yield return new WaitForSeconds(waitTime);

        
        yield return new WaitForSeconds(0.6f);

        controller.open = false;

        yield return new WaitForSeconds(1);

        noiseProfile.m_AmplitudeGain = 0.25f;
        rgb.velocity = new Vector2(speed, 0);
        Arrive = false;

    }

    IEnumerator Arriving()
    {
        yield return new WaitForSeconds(5);

        Arrive = true;
    }
}
