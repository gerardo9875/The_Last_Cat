using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelCollider : MonoBehaviour
{
    public string levelName;
    public GameObject fade;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(changeScene());
        }
    }

    IEnumerator changeScene()
    {
        fade.SetActive(true);
        Animator anim = fade.GetComponent<Animator>();
        anim.Play("FadeOut");

        GameObject player = GameObject.Find("Player");
        Destroy(player.GetComponent<Player_Movement>());
        Destroy(player.GetComponent<Player_Disparo>());

        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene(levelName);
    }
}
