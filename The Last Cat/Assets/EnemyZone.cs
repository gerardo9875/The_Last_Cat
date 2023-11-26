using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.Windows.Speech;

public class EnemyZone : MonoBehaviour
{
    [Header("Contador de enemigos")]
    [SerializeField] EnemyCounter Contador;

    [SerializeField] CinemachineVirtualCamera camara;
    [SerializeField] Animator[] Tiles;
    [SerializeField] GameObject[] DoorsToClose;
    [SerializeField] GameObject EnemiesToHide;
    [SerializeField] GameObject Director;

    bool PlayerInArea = false;

    private void Awake()
    {
        Director.SetActive(false);
    }
    private void Update()
    {
        if (PlayerInArea && !Contador.AllEnemiesKilled)
        {
            camara.Priority = 11;

            //Esconder a los enemigos de otras zonas
            EnemiesToHide.SetActive(false);

            //Esconder los tiles de otras zonas
            for (int i = 0; i < Tiles.Length; i++) Tiles[i].SetBool("Active", true);

            //Animaciones de las puertas y encerrar al jugador
            for (int i = 0; i < DoorsToClose.Length; i++)
            {
                Animator anim = DoorsToClose[i].GetComponent<Animator>();
                Collider2D coll = DoorsToClose[i].GetComponent<Collider2D>();

                anim.SetBool("Active", true);
                coll.enabled = true;
            }

            //Animacion para mostrar a los enemigos
            Director.SetActive(true); 
        }
        else
        {
            camara.Priority = 0;

            //Mostrar de nuevo a los enemigos ocultos
            EnemiesToHide.SetActive(true);

            //Mostrar los tiles de otras zonas
            for (int i = 0; i < Tiles.Length; i++) Tiles[i].SetBool("Active", false);

            //Animaciones de las puertas y encerrar al jugador
            for (int i = 0; i < DoorsToClose.Length; i++)
            {
                Animator anim = DoorsToClose[i].GetComponent<Animator>();
                Collider2D coll = DoorsToClose[i].GetComponent<Collider2D>();

                anim.SetBool("Active", false);
                coll.enabled = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInArea = true;
        }
    }

}
