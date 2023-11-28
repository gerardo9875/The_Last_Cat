using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hordas : MonoBehaviour
{

    public int hordas;
    
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = hordas.ToString("0");
    }

    public void AumentarHorda(int horda)
    {
        hordas += horda;
    }
}
