using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class platformLogic : MonoBehaviour
{
    //Platform variables.
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
    private int numberOfEnemies;
    private float maxPlayerBuffer;
    private float playerBuffer;
    private bool playerBufferFlag;
    private int playerPlatformHop;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Intialize all variables.
        thisPlatform = this.gameObject;
        sprite = thisPlatform.GetComponentInChildren<SpriteRenderer>();
        thisCollider = thisPlatform.GetComponentInChildren<BoxCollider2D>();
        originalPlatformColor = sprite.color;
        platFormColor = sprite.color;

        respawning = false;
        respawnTimer = maxRespawnTimer;
        maxPlayerBuffer = 1f;
        playerBuffer = maxPlayerBuffer;
        playerBufferFlag = false;
        playerPlatformHop = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Countdown playerBuffer.
        if (playerBufferFlag && playerBuffer > 0)
        {
            playerBuffer = playerBuffer - Time.deltaTime;
        }
        else if (playerBufferFlag && playerBuffer <= 0 && !countingDown || numberOfEnemies == 6 && !countingDown || playerPlatformHop == 3 && !countingDown) //start the despawn timer if any of these 3 conditions have been met.
        {
            startPlatformCountdown();
        }

        //Once the player buffer has been completed. Countdown the platform despawn timer.
        if(countingDown == true){
            platformTimerUpdate();
        }
        else if (respawning == true) //Once a platform has despawned. Countdown the repsawn timer.
        {
            respawnTimerUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;

        //Initiate player buffering coutner.
        if (layer == 3 && !playerBufferFlag)
        {
            playerBuffer = maxPlayerBuffer;
            playerBufferFlag = true;
        }
        else if (!countingDown && layer == 6){ //Track number of nemies on platform.
            numberOfEnemies = numberOfEnemies + 1;
        }

        if (layer == 3 && playerPlatformHop < 3)
        {
            playerPlatformHop ++;
        }

        /*if (!countingDown && layer == 3 || !countingDown && numberOfEnemies >= 5)
        {
            startPlatformCountdown();
        }
        else if (!countingDown && layer == 6){
            numberOfEnemies = numberOfEnemies + 1;
        }*/
    }

    //OnCollisionStay2D is called once per fixed update NO once per frame.
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        //Initialize player layer variable.
        int layer = collision.gameObject.layer;

        //Reduce the player buffer timer.
        if (layer == 3 && !playerBufferFlag)
        {
            playerBufferFlag = true;
            startPlatformCountdown();
        }     
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Initialize player layer variable.
        int layer = collision.gameObject.layer;

        if (layer == 3)
        {
            playerBuffer = maxPlayerBuffer;
            playerBufferFlag = false;
        }
        else if (layer == 6 && numberOfEnemies > 0)
        {
            numberOfEnemies --;
        }
    }

    private void startPlatformCountdown()
    {
        countingDown = true;
        platformTimer = maxPlatformTimer;
    }

    private void resetPlatformCountdown()
    {
        //Reset platform timers;
        countingDown = false;
        respawning = true;
        respawnTimer = maxRespawnTimer;
        numberOfEnemies = 0;

        //Reset player platform buffer flags.
        playerBufferFlag = false;
        playerPlatformHop = 0;
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

    private void updatePlayerBuffer()
    {
        playerBuffer = playerBuffer - Time.fixedDeltaTime;
    }

}
