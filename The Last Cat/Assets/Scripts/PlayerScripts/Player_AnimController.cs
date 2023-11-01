using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimController : MonoBehaviour
{
    Player_Movement mov;
    Player_Disparo shoot;
    Player_Orientacion orientacion;

    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        mov = GetComponent<Player_Movement>();
        shoot = GetComponentInChildren<Player_Disparo>();
        orientacion = GetComponentInChildren<Player_Orientacion>();
    }

    private void Update()
    {
        playerAnimator.SetBool("isShooting", shoot.isShooting);

        playerAnimator.SetFloat("Speed", mov.moveInput.sqrMagnitude);

        if (!shoot.isShooting && !mov.isdashing)
        {
            playerAnimator.SetFloat("Horizontal", mov.moveInput.x);
            playerAnimator.SetFloat("Vertical", mov.moveInput.y);
        }
        else if (shoot.isShooting)
        {
            playerAnimator.SetFloat("Horizontal", orientacion.direccion.normalized.x);
            playerAnimator.SetFloat("Vertical", orientacion.direccion.normalized.y);
        }

    }
}
