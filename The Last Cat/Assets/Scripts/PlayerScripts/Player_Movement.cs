using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D playerRb;

    [Header("Movimiento")]
    [SerializeField] private float speed;
    [NonSerialized] public Vector2 moveInput;
    [NonSerialized] public Vector2 lastInput;
    [NonSerialized] public bool canMove = true;

    [Header("Dash/Rodada")]
    public float distancedash;
    public float duraciondash;
    public float cooldowndash;
    private bool candash = true;
    [NonSerialized] public bool isdashing = false;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();

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
        playerRb.velocity = lastInput.normalized * distancedash / duraciondash;
        yield return new WaitForSeconds(duraciondash);
        isdashing = false;
        playerRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(cooldowndash);
        candash = true;
    }
}
