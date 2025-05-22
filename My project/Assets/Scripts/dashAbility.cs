using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class dashAbility : abilityBase
{
    playerCoroutines coroutines;

    [SerializeField] private float dashSpeed;

    public override void Activate(GameObject parent)
    {
        coroutines = parent.GetComponent<playerCoroutines>();

        Debug.Log("Ability Activated.");

        coroutines.startDash(activeTime, dashSpeed);
    }

    //Old dash coroutine.
    /*private IEnumerator Dash()
    {
        Debug.Log("Start Dash");
        //Initialize routine varaibles.
        float originalGravity = rig.gravityScale;
        Vector2 originalVelocity = new Vector2(playerController.directionalMemory * playerController.moveSpeed, 0f); //Capture the current x velocity.

        //Set tracking variables.
        playerController.canDash = false;
        playerController.isDashing = true;

        //Set dash variables.
        rig.gravityScale = 0f;
        rig.linearVelocity = new Vector2(playerController.directionalMemory * playerController.dashSpeed, 0f); //Dash according to the last known direction.
        Debug.Log(rig.linearVelocity.x);

        //Wait for a certain amount of seconds.
        yield return new WaitForSeconds(playerController.dashTime);

        //Reset dash and tracking variables.
        rig.gravityScale = originalGravity;
        rig.linearVelocity = originalVelocity; //return the characters velocity to what it was pre-dash.
        playerController.isDashing = false;
        playerController.dashInput = false;

        yield return new WaitForSeconds(playerController.dashCooldown);

        //Reset tracking variables.
        playerController.canDash = true;
        playerController.dashInput = false;
    }*/
}
