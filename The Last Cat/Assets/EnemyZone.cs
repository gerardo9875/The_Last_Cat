using UnityEngine;
using Cinemachine;

public class EnemyZone : MonoBehaviour
{
    [Header("Contador de enemigos")]
    [SerializeField] EnemyCounter Contador;

    [SerializeField] CinemachineVirtualCamera camara;
    [SerializeField] Animator[] animators;
    [SerializeField] GameObject EnemiesToHide;

    public bool PlayerInArea = false;
    private void Update()
    {
        if (PlayerInArea && !Contador.AllEnemiesKilled)
        {
            camara.Priority = 11;
            EnemiesToHide.SetActive(false);

            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool("Active", true);
            }
        }
        else
        {
            camara.Priority = 0;
            EnemiesToHide.SetActive(true);

            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool("Active", false);
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
