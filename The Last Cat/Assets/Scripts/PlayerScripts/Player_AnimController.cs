using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimController : MonoBehaviour
{
    Player_Movement mov;
    Player_Disparo shoot;
    Player_Orientacion orientacion;

    [NonSerialized] public Animator playerAnimator;

    public Vector2 NoMoveDir;
    bool canChangeDir;

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
        playerAnimator.SetBool("isDashing", mov.isdashing);



        if (mov.isdashing && canChangeDir)
        {
            NoMoveDir = mov.lastInput;
            canChangeDir = false;
        }
        else if (mov.gettingDamage && canChangeDir)
        {
            NoMoveDir = mov.lastInput;
            canChangeDir = false;
        }
        else if (!mov.gettingDamage && !mov.isdashing)
        {
            canChangeDir = true;
        }



        if (!shoot.isShooting && !mov.isdashing)
        {
            playerAnimator.SetFloat("Horizontal", mov.lastInput.x);
            playerAnimator.SetFloat("Vertical", mov.lastInput.y);
        }
        else if (shoot.isShooting)
        {
            playerAnimator.SetFloat("Horizontal", orientacion.direccion.normalized.x);
            playerAnimator.SetFloat("Vertical", orientacion.direccion.normalized.y);
        }
        else if (mov.isdashing)
        {
            playerAnimator.SetFloat("Horizontal", NoMoveDir.x);
            playerAnimator.SetFloat("Vertical", NoMoveDir.y);
        }
    }
}
