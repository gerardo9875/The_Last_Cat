using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelCollider : MonoBehaviour
{
    public string levelName;
    public GameObject fade;
    GameObject HUD;

    private void Update()
    {
        if (HUD == null && GameObject.Find("HUD")) HUD = GameObject.Find("HUD");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(changeScene());
        }
    }

    IEnumerator changeScene()
    {
        HUD.SetActive(false);
        fade.SetActive(true);
        Animator anim = fade.GetComponent<Animator>();
        anim.Play("FadeOut");

        GameObject player = GameObject.Find("Player");
        Destroy(player.GetComponent<Player_Movement>());
        Destroy(player.GetComponent<Player_Disparo>());

        Rigidbody2D rgb = player.GetComponent<Rigidbody2D>();
        rgb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene(levelName);
    }
}
