using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EstationStop : MonoBehaviour
{
    [SerializeField] Respawn spawner;
    [SerializeField] DoorsController controller;
    [SerializeField] GameObject outsideLight;
    Rigidbody2D rgb;

    [Header("Camara")]
    public CinemachineVirtualCamera VirtualCamera;

    private CinemachineBasicMultiChannelPerlin noiseProfile;

    public bool Arrive;
    public float speed;
    public float waitTime;
    bool canArrive = true;


    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();

        noiseProfile = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noiseProfile.m_AmplitudeGain = 0.25f;
    }

    private void Update()
    {
        if(Arrive)
        {
            if (canArrive)
            {

                if(transform.position.x <= 0)
                {
                    rgb.velocity = new Vector2(speed, 0);
                }
                else
                {
                    noiseProfile.m_AmplitudeGain = 0f;
                    StartCoroutine(EnemiesLandingTime());
                }

            }
        }

        if(transform.position.x >= 100)
        {
            canArrive = true;

            Vector2 pos = transform.position;
            pos.x = -100;
            transform.position = pos;

            rgb.velocity = new Vector2(0, 0);
        }
    }

    IEnumerator EnemiesLandingTime()
    {
        controller.open = true;
        canArrive = false;

        rgb.velocity = new Vector2(0, 0);

        spawner.canSpawn = true;

        yield return new WaitForSeconds(waitTime);

        
        yield return new WaitForSeconds(0.6f);

        controller.open = false;

        yield return new WaitForSeconds(1);

        noiseProfile.m_AmplitudeGain = 0.25f;
        rgb.velocity = new Vector2(speed, 0);
        Arrive = false;

    }
}
