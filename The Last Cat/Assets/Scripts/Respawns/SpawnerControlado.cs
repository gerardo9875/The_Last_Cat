using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnerControlado : MonoBehaviour
{
    private TextMeshProUGUI txt;

    [SerializeField] EstationStop station;
    [SerializeField] GameObject lataDeAtun;
    [SerializeField] Transform PosicionLata;
    bool canInstatiate = true;

    [SerializeField] private GameObject[] enemies1;
    [SerializeField] private GameObject[] enemies2;
    [SerializeField] private GameObject[] enemies3;
    [SerializeField] private GameObject[] enemies4; 
    [SerializeField] private GameObject[] enemies5;
    [SerializeField] private GameObject[] enemies6;

    public GameObject[] currentEnemies;

    [NonSerialized] public List<GameObject[]> arrayLists = new List<GameObject[]>();

    public int horda = 0;
    [NonSerialized] public bool canSpawn;
    [NonSerialized] public int spawnLimit;
    [NonSerialized] public int spawnCount;
    public int sceneNum;

    private void Start()
    {
        arrayLists.Add(enemies1);
        arrayLists.Add(enemies2);
        arrayLists.Add(enemies3);
        arrayLists.Add(enemies4);
        arrayLists.Add(enemies5);
        arrayLists.Add(enemies6);

        currentEnemies = arrayLists[horda];

        txt = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(station.enemyCounter >= currentEnemies.Length)
        {
            horda++;
            txt.text = horda.ToString("0");
        }

        if(horda != 0 && horda % 3 == 0 && canInstatiate && horda < arrayLists.Count)
        {
            Instantiate(lataDeAtun, PosicionLata.position, PosicionLata.rotation);
            canInstatiate = false;
        }


        if (canSpawn && horda < arrayLists.Count)
        {
            for (int i  = 0; i < currentEnemies.Length; i++)
            {
                GameObject enemytoSpawn = currentEnemies[i];
                Instantiate(enemytoSpawn, transform.position, Quaternion.identity);

                spawnCount++;

                if (spawnCount >= currentEnemies.Length)
                {
                    canSpawn = false;
                    currentEnemies = arrayLists[horda];
                    spawnCount = 0;

                }
            }
        }

        if (horda > arrayLists.Count)
        {
            StartCoroutine(ChangeScene());
        }

    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.5f);

        Animator anim = GameObject.Find("Fade").GetComponent<Animator>();
        anim.SetBool("Active", true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneNum); //Nombre de la siguiente escena
    }

}
