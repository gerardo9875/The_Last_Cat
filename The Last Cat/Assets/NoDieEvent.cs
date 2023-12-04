using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class NoDieEvent : MonoBehaviour
{
    [SerializeField] EnemyCounter contador;
    Player_Life life;

    [Header("Prefabs")]
    [SerializeField] GameObject[] NPC;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject lataDeAtun;

    public bool dead;

    private void Start()
    {
        contador = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();
        life = GameObject.Find("Player").GetComponent<Player_Life>();
    }

    private void Update()
    {
        if(life.currentlife == 1)
        {
            StartCoroutine(life.RecieveDamage(0));
            StartCoroutine(killEnemies());
        }
    }

    IEnumerator killEnemies()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < contador.enemies.Length; i++)
        {
            if (contador.enemies[i] != null)
            {
                if (contador.enemies[i].GetComponent<PerroZombie_Life>().alive == true)
                {

                    Vector3 dir = contador.enemies[i].transform.position - NPC[i].transform.position;
                    dir.Normalize(); ;
                    Quaternion rotacionRelativa = Quaternion.LookRotation(Vector3.forward, dir);

                    GameObject bala = Instantiate(bullet, NPC[i].transform.position, rotacionRelativa);
                    bala.GetComponent<Player_AlfaBullet>().tiempo = 10;

                    contador.enemies[i].GetComponent<PerroZombie_Life>().alive = false;
                    contador.addEnemy();
                }
            }

        }

        enabled = false;
    }
}
