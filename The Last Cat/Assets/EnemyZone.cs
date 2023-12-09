using UnityEngine;
using Cinemachine;

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
        if(Director != null) Director.SetActive(false);
    }
    private void Update()
    {
        if (PlayerInArea && !Contador.AllEnemiesKilled)
        {
            if(camara != null) camara.Priority = 11;

            //Esconder a los enemigos de otras zonas
            if(EnemiesToHide != null) EnemiesToHide.SetActive(false);

            //Esconder los tiles de otras zonas
            if(Tiles != null) for (int i = 0; i < Tiles.Length; i++) Tiles[i].SetBool("Active", true);

            //Animaciones de las puertas y encerrar al jugador
            for (int i = 0; i < DoorsToClose.Length; i++)
            {
                Animator anim = DoorsToClose[i].GetComponent<Animator>();
                Collider2D coll = DoorsToClose[i].GetComponent<Collider2D>();

                anim.SetBool("Active", true);
                coll.enabled = true;
            }

            //Animacion para mostrar a los enemigos
            if(Director != null) Director.SetActive(true); 
        }
        else
        {
            if (camara != null) camara.Priority = 0;

            //Mostrar de nuevo a los enemigos ocultos
            if(EnemiesToHide != null) EnemiesToHide.SetActive(true);

            //Mostrar los tiles de otras zonas
            if(Tiles != null) for (int i = 0; i < Tiles.Length; i++) Tiles[i].SetBool("Active", false);

            //Animaciones de las puertas y encerrar al jugador)
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
