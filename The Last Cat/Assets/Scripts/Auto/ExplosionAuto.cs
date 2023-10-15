using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAuto : MonoBehaviour
{
    [Header("Detección")]
    public float radioDetec;
    public Transform player;
    public LayerMask PlayerLayer;
    

    [Header("Explosion")]
    public GameObject Explosion;
    public float radioExpl;
    public float ContadorTiempo;


    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, radioExpl, PlayerLayer);
    }

    


    void Update()
    {

        if (PlayerInArea()) StartCoroutine(cuentaRegresiva());

    }

    IEnumerator cuentaRegresiva()
    {
        
        yield return new WaitForSeconds(ContadorTiempo);

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        Destroy(gameObject);


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExpl);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDetec);
    }
}
