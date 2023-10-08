using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActualizarNivel : MonoBehaviour
{

    private void Awake()
    {
        CambiarEscena.CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        print(CambiarEscena.CurrentLevel);
    }
}
