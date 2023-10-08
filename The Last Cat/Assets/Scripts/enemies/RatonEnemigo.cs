using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RatonEnemigo : MonoBehaviour
{
    public float ContadorTiempo = 3;
    public Transform player;
    public GameObject Explosion;
    bool detector;

    public float velocidad;
    public float radioExpl;
    public float radioDetec;

    bool canMove = true;

    public LayerMask PlayerLayer;

    bool PlayerInArea()
    {
        return Physics2D.OverlapCircle(transform.position, radioExpl, PlayerLayer);
    }
    bool PlayerDetetion()
    {
        return Physics2D.OverlapCircle(transform.position, radioDetec, PlayerLayer);
    }
    void Update()
    {
        if (PlayerDetetion())
        {
            detector = true;
        } 

        if(detector && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidad * Time.deltaTime);
        }

        if (PlayerInArea())
        {
            StartCoroutine(cuentaRegresiva());
        }
    }
    IEnumerator cuentaRegresiva()
    {
        canMove= false;
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
