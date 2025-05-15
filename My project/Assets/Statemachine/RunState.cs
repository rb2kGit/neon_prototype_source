using UnityEngine;

public class RunState : State
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter()
    {
        Debug.Log("Enter Run");
    }

    public override void stateUpdate()
    {
        if(playerInput.xInput == 0)
        {
            stateComplete = true;
        }
        else if(!playerInput.groundedCheck())
        {
            stateComplete = true;
        }
        else
        {
            moveHandler();
        }
    }

    private void moveHandler()
    {
        //Initialize local variables.
        float currentVelocity = rig.linearVelocity.x; //Create a reference variable for the current velocity.
        float accelerationCap = playerInput.accelSpeed * Time.fixedDeltaTime; //This varable will use the accelaration speed to create an accelartion cap in Mathf.MoveTowards, when combined with time.delta time.

        
        //Handle movement with acceleration when the player is pressing left or right while grounded.
        if(playerInput.groundedCheck() && playerInput.xInput != 0) //When the player is pressing left or right on the ground.
        {
            
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, playerInput.xInput * playerInput.moveSpeed, accelerationCap ), rig.linearVelocity.y);
            playerInput.xInputMemory = playerInput.xInput;
            playerInput.directionalMemory = playerInput.xInput;

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
