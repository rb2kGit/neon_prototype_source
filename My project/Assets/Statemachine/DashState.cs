using UnityEngine;
using System.Collections;

public class DashState : State
{
    public override void Enter()
    {
        Debug.Log("Enter Dash");
        playerInput.isDashing = true;
    }

    public override void stateUpdate()
    {
       if(!playerInput.isDashing)
       {
            stateComplete = true;
       }
       else
       {
            dashHandler();
       }
    }

    public override void Exit()
    {
        
    }

    private void dashHandler() 
    {
        
        if(playerInput.dashInput && playerInput.canDash) //Start Coroutine if the player has pressed the dash button this frame AND if dash is not on cooldown in a prevous coroutine.
        {
            StartCoroutine(Dash());
        }
        else
        {
            playerInput.dashInput = false; 
            //In the case that the If statement fails it means that either there is no dash input or dash is on cooldown.
            //This means that if dash is on cooldown, the dash input will fail. The player will have to use the key again.
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Start Dash");
        //Initialize routine varaibles.
        float originalGravity = rig.gravityScale;
        Vector2 originalVelocity = new Vector2(playerInput.directionalMemory * playerInput.moveSpeed, 0f); //Capture the current x velocity.

        //Set tracking variables.
        playerInput.canDash = false;
        playerInput.isDashing = true;
        
        //Set dash variables.
        rig.gravityScale = 0f;
        rig.linearVelocity = new Vector2(playerInput.directionalMemory * playerInput.dashSpeed, 0f); //Dash according to the last known direction.
        Debug.Log(rig.linearVelocity.x);

        //Wait for a certain amount of seconds.
        yield return new WaitForSeconds(playerInput.dashTime);

        //Reset dash and tracking variables.
        rig.gravityScale = originalGravity;
        rig.linearVelocity = originalVelocity; //return the characters velocity to what it was pre-dash.
        playerInput.isDashing = false;
        playerInput.dashInput = false;

        yield return new WaitForSeconds(playerInput.dashCooldown);

        //Reset tracking variables.
        playerInput.canDash = true;
    }
}
