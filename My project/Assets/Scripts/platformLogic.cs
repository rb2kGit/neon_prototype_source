using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class platformLogic : MonoBehaviour
{
    //Platformv ariables.
    private GameObject thisPlatform;
    private SpriteRenderer sprite;
    private BoxCollider2D thisCollider;
    private Color platFormColor;
    private Color originalPlatformColor;
    private float platformOpacity;

    private Boolean countingDown;
    [SerializeField] private float maxPlatformTimer;
    [SerializeField] private float maxRespawnTimer;
    private float platformTimer;
    private float respawnTimer;
    private bool respawning;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisPlatform = this.gameObject;
        sprite = thisPlatform.GetComponentInChildren<SpriteRenderer>();
        thisCollider = thisPlatform.GetComponentInChildren<BoxCollider2D>();
        originalPlatformColor = sprite.color;
        platFormColor = sprite.color;

        respawning = false;
        respawnTimer = maxRespawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(countingDown == true){
            platformTimerUpdate();
        }
        else if (respawning == true)
        {
            respawnTimerUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        
        if (!countingDown && layer == 3)
        {
            startPlatformCountdown();
        }
    }

    private void startPlatformCountdown()
    {
        countingDown = true;
        platformTimer = maxPlatformTimer;
    }

    private void resetPlatformCountdown()
    {
        countingDown = false;
        respawning = true;
        respawnTimer = maxRespawnTimer;
    }

    private void disablePlatform()
    {
        //Destroy(gameObject);
        respawning = true;
        sprite.enabled = false;
        thisCollider.enabled = false;
    }

    private void platformTimerUpdate()
    {
        if(platformTimer > 0)
        {
            platformTimer = platformTimer - Time.deltaTime;

            platformOpacity = 0f + (platformTimer / maxPlatformTimer);
            platFormColor.a = Mathf.Clamp01(platformOpacity);
            sprite.color = platFormColor;

        }
        else if (platformTimer <= 0)
        {
            resetPlatformCountdown();
            disablePlatform();
        }
    }

    private void resetPlatformRespawn()
    {
        respawning = false;
        respawnTimer = maxRespawnTimer;
        sprite.enabled = true;
        thisCollider.enabled = true;
        sprite.color = originalPlatformColor;
    }

    private void respawnTimerUpdate()
    {
        if(respawnTimer > 0)
        {
            respawnTimer = respawnTimer - Time.deltaTime;
        }
        else if (platformTimer <= 0f)
        {
            resetPlatformRespawn();
        }
    }
}
