using UnityEngine;

public class Vida : MonoBehaviour
{
    public GameObject[] vidas;
    Controlador control;

    private void Start()
    {
        
        if (control != null)
        {
            if (GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>())
            {
                control = GameObject.FindGameObjectWithTag("Controlador").GetComponent<Controlador>();
                Life(control.life);
            }
        }

        else
        {
            Life(7);
        }
        
    }
    public void Life(int actualizar)
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            Animator anim = vidas[i].GetComponent<Animator>();
            if (anim.enabled == true)
            {
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
