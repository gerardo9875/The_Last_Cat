using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PerroZombi_Atack : MonoBehaviour
{
    [SerializeField]float radio;
    [SerializeField] Transform attackPos;
    [SerializeField] Transform playertransform;

    Vector2 playerpos;


    [SerializeField] Enemy_Detection deteccionScript;

    bool AttackRaycast()
    {
        return Physics2D.OverlapCircle(attackPos.position, radio, deteccionScript.PlayerLayer);
    }

    private void Update()
    {
        playerpos = playertransform.position;

        float anguloRad = Mathf.Atan2(playerpos.y - transform.position.y, playerpos.x - transform.position.x);
        float anguloDeg = anguloRad * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, anguloDeg);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, radio);
    }
}
