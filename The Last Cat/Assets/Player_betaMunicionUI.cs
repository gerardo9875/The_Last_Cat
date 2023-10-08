using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_betaMunicionUI : MonoBehaviour
{
    [SerializeField] Player_Disparo disparoScript;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.fillAmount = disparoScript.BetaCurrentAmmo / disparoScript.BetaMaxAmmo;
    }
}
