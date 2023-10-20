using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    Player_Movement mov;
    Player_Disparo shoot;

    [SerializeField] int maxLife;
    public int currentlife;

    private void Awake()
    {
        mov = GetComponent<Player_Movement>();
        shoot = GetComponentInChildren<Player_Disparo>();
    }

    private void Start()
    {
        currentlife = maxLife;
    }
    private void Update()
    {
        if (currentlife <= 0)
        {
            mov.canMove = false;
            shoot.canShoot = false;

            Rigidbody2D rgb = GetComponent<Rigidbody2D>();
            rgb.velocity = Vector3.zero;

            //StartCoroutine(ChangeScene());
        }

    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("GameOver1");
    }
}
