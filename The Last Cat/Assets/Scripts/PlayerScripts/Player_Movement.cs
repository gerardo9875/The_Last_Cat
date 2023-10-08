using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D playerRb;
    private Animator playerAnimator;

    Vector2 moveInput;

    [SerializeField] private float speed;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

       
    }
    
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Diagonal",moveX, moveY, Time.deltaTime);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position+moveInput *speed * Time.fixedDeltaTime);
    }
}
