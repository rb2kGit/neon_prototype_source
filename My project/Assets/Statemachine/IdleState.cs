using Unity.VisualScripting;
using UnityEngine;

public class IdleState : State
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter()
    {
        Debug.Log("Enter Idle");
    }

    public override void stateUpdate()
    {
        if(playerInput.xInput != 0)
        {
            stateComplete = true;
        }
        else if(!playerInput.groundedCheck())
        {
            stateComplete = true;
        }
        else
        {
            idleHandler();
        }
    }

    private void idleHandler()
    {
        float currentVelocity = rig.linearVelocity.x; //Create a reference variable for the current velocity.
        float deccelerationCap = playerInput.decelSpeed * Time.fixedDeltaTime; //This varable will use the decceleration speed to create an decceleration cap in Mathf.MoveTowards, when combined with time.delta time.

        //When the player is not presing left or right on the ground.
        if(playerInput.groundedCheck())
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, 0, deccelerationCap ), rig.linearVelocity.y); //Add deceleration to stop the player from moving.
            playerInput.xInputMemory = playerInput.xInput;
        }

        //When the jump input is pressed while on the ground.
        if(playerInput.jumpInput && playerInput.groundedCheck())
        {
            jumpController.jumpHandler();
        }
    }

    public override void Exit()
    {
        
    }
}
