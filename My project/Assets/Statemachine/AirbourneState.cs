using System;
using UnityEngine;

public class AirbourneState : State
{

    //Jump variables.
    public float jumpSpeed;

    public override void Enter()
    {
        Debug.Log("Enter Airbourne");
    }

    public override void stateUpdate() // <-- LLO converting player controller into stae machine.
    {
        //The only way this state is complete is if the player is no longer airbourne.
        if(playerInput.groundedCheck())
        {
            stateComplete = true;
        }
    }

    public override void Exit()
    {
        
    }
}
