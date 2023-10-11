using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RatonEnemigo : MonoBehaviour
{
    [Header("Detección")]
    public float radioDetec;
    public Transform player;
    public LayerMask PlayerLayer;
    private bool detector;

    [Header("Movimiento")]
    public float velocidad;
    private bool canMove = true;

    [Header("Explosion")]
    public GameObject Explosion;
    public float radioExpl;
    public float ContadorTiempo;


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

        Detection();

        if (PlayerInArea()) StartCoroutine(cuentaRegresiva());

    }


    private void Detection()
    {
        if (PlayerDetetion())
        {
            detector = true;
        }

        if (detector && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidad * Time.deltaTime);
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
