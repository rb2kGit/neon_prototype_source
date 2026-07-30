using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class arenaEventManager : MonoBehaviour
{
    private float startTime;
    private float eventTimer;
    [SerializeField] private float eventTriggerTime;
    [SerializeField] private GameObject platform1;
    [SerializeField] private GameObject platform2;
    [SerializeField] private GameObject platform3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
        eventTimer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Update event timer.
        eventTimer = eventTimer + Time.deltaTime;

        switch (eventTimer)
        {
            case float e when e > 90 && e < 92:
                StartCoroutine(movePlatforms());
                break;
            case float e when e > 270 && e < 272:
                StartCoroutine(movePlatforms());
                break;
            case float e when e > 450 && e < 452:
                StartCoroutine(movePlatforms());
                break;
        }
    }

    public IEnumerator movePlatforms()
    {
        Vector3 platform1NewPos = new Vector3(-13, 5, 0);
        Vector3 platform2NewPos = new Vector3(13, 5, 0);
        Vector3 platform3NewPos = new Vector3(0, 0, 0);

        while (platform1.transform.position != platform1NewPos)
        {
            platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, platform1NewPos, 0.005f * Time.deltaTime);
            platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, platform2NewPos, 0.005f * Time.deltaTime);
            platform3.transform.position = Vector3.MoveTowards(platform3.transform.position, platform3NewPos, 0.005f * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(60);

        StartCoroutine(returnPlatforms());

        yield return new WaitForEndOfFrame();
        
    }

    public IEnumerator returnPlatforms()
    {
        Vector3 platform1NewPos = new Vector3(-13, 0, 0);
        Vector3 platform2NewPos = new Vector3(13, 0, 0);
        Vector3 platform3NewPos = new Vector3(0, 5, 0);

        while (platform1.transform.position != platform1NewPos)
        {
            platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, platform1NewPos, 0.005f * Time.deltaTime);
            platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, platform2NewPos, 0.005f * Time.deltaTime);
            platform3.transform.position = Vector3.MoveTowards(platform3.transform.position, platform3NewPos, 0.005f * Time.deltaTime);

            yield return null;
        }
        
    }
}
