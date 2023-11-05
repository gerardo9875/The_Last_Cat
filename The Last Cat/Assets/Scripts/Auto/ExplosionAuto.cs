using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAuto : MonoBehaviour
{
    SpriteRenderer Renderer;
    [SerializeField] Animator animator;
    [SerializeField] GameObject children;

    [SerializeField] Sprite carDestroyedSprite;

    [Header("Detección")]
    public float radioDetec;
    public Vector3 offset;
    public LayerMask PlayerLayer;
    

    [Header("Explosion")]
    public GameObject Explosion;
    public float ContadorTiempo;
    bool canAdd = true;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position + offset, radioDetec, PlayerLayer);
    }

    


    void Update()
    {

        if (PlayerInArea() & canAdd)
        {
            canAdd = false;
            StartCoroutine(cuentaRegresiva());
        }

    }

    IEnumerator cuentaRegresiva()
    {
        animator.SetBool("Active", true);


        yield return new WaitForSeconds(ContadorTiempo);

        radioDetec = 0;
        children.SetActive(false);

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        Renderer.sprite = carDestroyedSprite;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offset, radioDetec);
    }
}
