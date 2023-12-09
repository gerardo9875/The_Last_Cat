using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bombaRaton : MonoBehaviour
{
    [SerializeField] float Contador;
    [SerializeField] GameObject Explosion;

    Rigidbody2D rgb;
    Animator anim;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        animationsVoid();

        Contador -= Time.deltaTime;
        if (Contador <= 0)
        {
            AutoDestroy();
        }
    }
    void AutoDestroy()
    {
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }


    void animationsVoid()
    {
        anim.SetFloat("X", rgb.velocity.x);
        anim.SetFloat("Y", rgb.velocity.y);
    }
}
