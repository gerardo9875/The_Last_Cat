using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D playerRb;
    Player_Disparo shootScript;
    Player_Life lifeScript;

    [Header("Movimiento")]
    [SerializeField] private float speed;
    [NonSerialized] public Vector2 moveInput;
    [NonSerialized] public Vector2 lastInput;
    public bool canMove = true;

    [Header("Dash/Rodada")]
    public float distancedash;
    public float duraciondash;
    public float cooldowndash;
    private bool candash = true;
    [NonSerialized] public bool isdashing = false;

    [Header("Damage")]
    [NonSerialized] public bool gettingDamage;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        shootScript = GetComponentInChildren<Player_Disparo>();
        lifeScript = GetComponent<Player_Life>();

        lastInput = new Vector2(0, -1);
    }
    
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space) && candash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {

        if(!isdashing && canMove)
        {
            playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
        }
        
    }

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY);

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            lastInput = moveInput;
        }
    }

    IEnumerator Dash()
    {
        candash = false;
        isdashing = true;
        shootScript.isShooting = false;
        shootScript.luzDisparo.SetActive(false);
        lifeScript.canRecieveDamage = false;

        playerRb.velocity = lastInput.normalized * distancedash / duraciondash;

        yield return new WaitForSeconds(duraciondash);
        isdashing = false;
        shootScript.canShoot = true;
        lifeScript.canRecieveDamage = true;
        playerRb.velocity = Vector2.zero;

        yield return new WaitForSeconds(cooldowndash);
        candash = true;
    }
    //-----------------------------------------------------------------------------------------
    // VOIDS DE DAÑO
    public void DamageFeedback(Vector2 dir)
    {
        gettingDamage = true;

        
        canMove = false;
        candash = false;
        isdashing = true;
        shootScript.canShoot = false;

        Player_AnimController anim = GetComponent<Player_AnimController>();
        anim.playerAnimator.Play("Damage");

        StopCoroutine(Dash());

        dir.Normalize();
        playerRb.velocity = dir * -5;
    }

    public void BecomeNormalState()
    {
        canMove = true;
        candash = true;
        shootScript.canShoot = true;
        isdashing = false;
        gettingDamage = false;

    }


}
