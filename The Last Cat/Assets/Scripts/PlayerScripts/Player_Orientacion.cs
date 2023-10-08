using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Orientacion : MonoBehaviour
{
    [SerializeField] Player_Movement mov;
    Animator animator;
    Camera cam;

    public Vector2 direccion;
    private Vector2 mousePos;

    int orientacion;
    public string orientacion2;
    public float anguloDeg;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        direccion = mousePos - mov.playerRb.position;

        float anguloRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        anguloDeg = anguloRad * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, anguloDeg);

        direccion.Normalize();

        //animator.SetInteger("", direction);

        if (anguloDeg < 70 && anguloDeg > 30) orientacion2 = "UpLeft";
        else if (anguloDeg < 30 && anguloDeg > -30) orientacion2 = "Up";
        else if (anguloDeg < -30 && anguloDeg > -70) orientacion2 = "UpRight";
        else if (anguloDeg < -70 && anguloDeg > -110) orientacion2 = "Right";
        else if (anguloDeg < -110 && anguloDeg > -150) orientacion2 = "DownRight";
        else if (anguloDeg < -150 && anguloDeg > -220) orientacion2 = "Down";
        else if (anguloDeg < -220 && anguloDeg > -250) orientacion2 = "DownLeft";
        else orientacion2 = "Left";
    }
}
