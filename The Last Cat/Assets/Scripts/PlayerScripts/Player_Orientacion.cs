using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Orientacion : MonoBehaviour
{
    [SerializeField] Player_Movement mov;
    Camera cam;

    public Vector2 direccion;
    private Vector2 mousePos;

    float anguloDeg;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        direccion = mousePos - mov.playerRb.position;

        float anguloRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        anguloDeg = anguloRad * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, anguloDeg);
    }

}
