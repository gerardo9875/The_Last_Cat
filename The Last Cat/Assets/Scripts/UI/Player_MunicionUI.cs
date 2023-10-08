using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_MunicionUI : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] Player_Disparo disparoScript;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int currentAmmo = disparoScript.AlfaCurrentAmmo;
        int maxAmmo = disparoScript.AlfaMaxAmmo;

        string format = "{00}/{1}";
        text.SetText(format, currentAmmo, maxAmmo);
    }
}
