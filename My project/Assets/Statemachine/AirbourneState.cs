using System;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class AirbourneState : State
{

    public override void Enter()
    {
        
    }

    public override void stateUpdate()
    {
        //The only way this state is complete is if the player is no longer airbourne.
        if(playerInput.groundedCheck())
        {
            playerInput.jumpCut = false;
            stateComplete = true;
        }
        else
        {
            airMoveHandler();
        }
    }


    private void airMoveHandler()
    {
        //Initialize air movement variables.
        float currentVelocity = rig.linearVelocity.x; //Create a reference variable for the current velocity.
        float airDampening = playerInput.accelSpeed * 0.5f * Time.deltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.
        float stillAirDampening = playerInput.accelSpeed * 0.1f * Time.deltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.

        //If the player presses jump whole aibourne, call the jumphandler ot check it.
        if(playerInput.jumpInput)
        {
            jumpController.jumpHandler();
        }

        //Apply jump cut velocity if space is not pressed and the y velocity is positive.
        if(playerInput.jumpCut && rig.linearVelocity.y > 0 && !playerInput.groundedCheck())
       {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, rig.linearVelocity.y * 0.60f);
            playerInput.jumpCut = false;
       }
        else if(playerInput.downForceInput && rig.linearVelocity.y <= 0 && !playerInput.groundedCheck()) //Apply downforce when the player is at the peak of the jump or falling.
       {
            rig.AddForce(transform.up * -playerInput.downForce);
       }
       else
       {
            playerInput.downForceInput = false; //Reset the downforce input before the player hits the ground.
       }

        //Movement controls in the air.
       if(playerInput.xInput == 0) //When the player is not pressing left or right in the air.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, playerInput.xInputMemory * playerInput.moveSpeed, airDampening ), rig.linearVelocity.y);
            
        }
        else if(rig.linearVelocity.x < 0.5 * playerInput.moveSpeed && rig.linearVelocity.y > 0) //When the player is rising with a slow speed, heavily dampen x velocity. Will make standstill jumping feel more realistic.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, playerInput.xInputMemory * playerInput.moveSpeed, stillAirDampening ), rig.linearVelocity.y);
            playerInput.xInputMemory = playerInput.xInput;
            playerInput.directionalMemory = playerInput.xInput;

        }
        else //When the player is in the air.
        {   
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, playerInput.xInput * playerInput.moveSpeed, airDampening ), rig.linearVelocity.y);
            playerInput.xInputMemory = playerInput.xInput;
            playerInput.directionalMemory = playerInput.xInput;

        }
      
    }

    public override void Exit()
    {
        
    }
}
