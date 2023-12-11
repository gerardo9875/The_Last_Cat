using UnityEngine;

public class Vida : MonoBehaviour
{
    public GameObject[] vidas;
    Controlador control;

    Player_Life life;

    private void Start()
    {
        life = GameObject.Find("Player").GetComponent<Player_Life>();

        if (control != null)
        {
            if (GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>())
            {
                control = GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>();
            }
        }
        
    }

    private void Update()
    {
        Life(life.currentlife);
    }

    public void Life(int actualizar)
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            if (vidas[i].activeInHierarchy == true)
            {
                Animator anim = vidas[i].GetComponent<Animator>();
                anim.SetBool("Active", i >= actualizar);
            }
        }
    }

    public void NewLIfe()
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            Animator anim = vidas[i].GetComponent<Animator>();
            anim.SetBool("Active", false);

            if (vidas[i].activeInHierarchy == false)
            {
                vidas[i].SetActive(true);
                break;
            }
        }
    }
}
