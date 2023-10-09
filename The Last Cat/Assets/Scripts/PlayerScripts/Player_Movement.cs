using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D playerRb;
    private Animator playerAnimator;

    [NonSerialized] public Vector2 moveInput;

    [SerializeField] private float speed;

    public float distancedash;
    public float duraciondash;
    public float cooldowndash;
    private bool candash = true;
    private bool isdashing = false;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

    }
    
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.V) && candash)
        {
            StartCoroutine(Dash());
        }

        //playerAnimator.SetFloat("Horizontal", moveX);
        //playerAnimator.SetFloat("Vertical", moveY);
        //playerAnimator.SetFloat("Diagonal",moveX, moveY, Time.deltaTime);
        //playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

    }

    private void FixedUpdate()
    {

        if(!isdashing)
        {
            playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
        }
        
    }

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY);
    }

    IEnumerator Dash()
    {
        candash = false;
        isdashing = true;
        playerRb.velocity = moveInput.normalized * distancedash / duraciondash;
        yield return new WaitForSeconds(duraciondash);
        isdashing = false;
        playerRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(cooldowndash);
        candash = true;
    }
}
