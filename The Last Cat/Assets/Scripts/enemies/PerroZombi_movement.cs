using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;

public class PerroZombi_movement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rgb;

    [Header("Player Detection")]
    [SerializeField] float DetectionRadius;
    [SerializeField] float UnfollowDelay;
    [SerializeField] LayerMask playerLayer;
    private float CurrentTime;
    private bool Deteccion;

    [Header("Movimiento")]
    [SerializeField] GameObject target;
    [SerializeField] float speed;
    [SerializeField] float minDistance;
    private bool canMove = true;
    private Vector3 dir;

    [Header("AtaqueMelee")]
    [SerializeField] float attackRadius;
    [SerializeField] Transform attackController;
    [SerializeField] Transform attackRaycastPos;
    [SerializeField] float attackDelay;
    [SerializeField] float passedTime;
    bool canAtack = true;
    bool shouldRotate = true;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, DetectionRadius, playerLayer);
    }
    bool AttackRaycast()
    {
        return Physics2D.OverlapCircle(attackRaycastPos.position, attackRadius, playerLayer);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        CurrentTime = UnfollowDelay;
    }

    private void Update()
    {

        animator.SetBool("IsWalking", Deteccion);

        Detection();
        Movement();
        Rotation();

    }

    private void Movement()
    {
        if (Deteccion && canMove)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (AttackRaycast() && canAtack)
                {
                    if (Attack() != null)
                    {
                        StartCoroutine(Attack());
                    }
                }
            }
        }
    }


    private void Detection()
    {
        if (PlayerInArea())
        {
            Deteccion = true;
            CurrentTime = UnfollowDelay;
        }
        else
        {
            CurrentTime -= Time.deltaTime;
        }

        if (CurrentTime < 0 && Deteccion)
        {
            Deteccion = false;
        }
    }


    private void Rotation()
    {
        if (shouldRotate)
        {
            dir = target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            attackController.rotation = Quaternion.Euler(0, 0, angle);

            dir.Normalize();

            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
        }
    }


    private IEnumerator Attack()
    {
        rgb.velocity = Vector2.zero;

        canAtack = false;
        shouldRotate = false;
        canMove = false;

        yield return new WaitForSeconds(passedTime);

        if (AttackRaycast())
        {
            Player_Life life = target.GetComponent<Player_Life>();
            life.currentlife--;
        }

        yield return new WaitForSeconds(passedTime * 3);
        rgb.velocity = Vector2.zero;
        canMove = true;
        shouldRotate = true;

        yield return new WaitForSeconds(attackDelay);
        canAtack = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackRaycastPos.position, attackRadius);
    }
}
