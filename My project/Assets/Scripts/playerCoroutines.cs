using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Vector2 = UnityEngine.Vector2;

public class playerCoroutines : MonoBehaviour
{
    //Player variables.
    public playerController playerController;
    public Rigidbody2D rig;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameObject dashPrefab;
    private Color originalColor;
    private bool playerImmune;

    //Start function for the dash coroutines.
    public void startDash(float dashTime, float dashSpeed)
    {
        StartCoroutine(Dash(dashTime, dashSpeed));
    }

    private IEnumerator Dash(float dashTime, float dashSpeed)
    {
        //Initialize routine varaibles.
        float originalGravity = rig.gravityScale;
        UnityEngine.Vector2 originalVelocity = new Vector2(playerController.directionalMemory * playerController.moveSpeed, 0f); //Capture the current x velocity.
        Vector2 startingPlayerPosition = playerObject.transform.position;

        //Set tracking variables.
        playerController.canDash = false;
        playerController.isDashing = true;

        //Set dash variables.
        rig.gravityScale = 0f;
        rig.linearVelocity = new Vector2(playerController.directionalMemory * dashSpeed, 0f); //Dash according to the last known direction.

        //Wait for a certain amount of seconds.
        yield return new WaitForSeconds(dashTime);

        //Initialize new variables based on the player's new position.
        Vector2 spawnPosition = playerObject.transform.position;
        float prefabLength = dashPrefab.GetComponent<BoxCollider2D>().size.x;
        float distanceTravelled = spawnPosition.x - startingPlayerPosition.x;
        spawnPosition.x = spawnPosition.x - (prefabLength / 2) * playerObject.transform.right.x;

        if (distanceTravelled > 0 && distanceTravelled >= (prefabLength * 0.5) || distanceTravelled < 0 && distanceTravelled <= (-prefabLength * 0.5))
        { 
            //Instantiate the damaging prefab.
            Instantiate(dashPrefab, spawnPosition, playerObject.transform.rotation);
        }

        //Reset dash and tracking variables.
        rig.gravityScale = originalGravity;
        rig.linearVelocity = originalVelocity; //return the characters velocity to what it was pre-dash.
        playerController.isDashing = false;
        playerController.ability2Input = false;
        playerController.canDash = true;
    }

    public IEnumerator bullCharge(Rigidbody2D bullRig, float direction, float bullSpeed, float chargeTime, GameObject bullObject)
    {
        yield return new WaitForSeconds(0.75f);

        bullRig.gravityScale = 0f;
        bullRig.linearVelocity = new Vector2(direction * bullSpeed, 0f);

        yield return new WaitForSeconds(chargeTime);

        bullRig.linearVelocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(1f);

        Destroy(bullObject);

    }

    public IEnumerator disablePlatformCollision(GameObject playerObject, GameObject platformObject)
    {
        
        CapsuleCollider2D playerCollider = playerObject.GetComponent<CapsuleCollider2D>();
        BoxCollider2D platformCollider = platformObject.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    public IEnumerator immuneRecovery()
    {
        //Debug.Log("I AM IMMUNE");
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int knockbackX = UnityEngine.Random.Range(0, 2) * 2 -1; //This formula will result in either -1 or 1 being selected because of multiplying by 2 and subtracting 1.

        if (!playerImmune)
        {
            rig.linearVelocity = new Vector2(0, 0);
            rig.AddForce(new Vector2(knockbackX, 1) * 20, ForceMode2D.Impulse);
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
            playerImmune = true;
            setPlayerAlpha(0.1f);
            yield return new WaitForSeconds(3f);
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            playerSprite.color = originalColor;
            playerImmune = false;
        }

    }

    private void setPlayerAlpha(float alphaValue)
    {
        Color currentColor = playerSprite.color; //Store the current color.
        originalColor = playerSprite.color;

        currentColor.a = Mathf.Clamp01(alphaValue); //Set the new color on the new variable.
        playerSprite.color = currentColor;
    }

    public bool getImmuneStatus()
    {
        return playerImmune;
    }

}
