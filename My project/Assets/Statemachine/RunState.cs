using UnityEngine;

public class RunState : State
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter()
    {
        //Debug.Log("Enter Run");
    }

    public override void stateUpdate() // <-- LLO converting player controller into state machine.
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
        /*float deccelerationCap = playerInput.decelSpeed * Time.fixedDeltaTime; //This varable will use the decceleration speed to create an decceleration cap in Mathf.MoveTowards, when combined with time.delta time.
        float airDampening = (playerInput.accelSpeed * 0.5f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.
        float stillAirDampening = (playerInput.accelSpeed * 0.1f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.*/


        if(playerInput.groundedCheck() && playerInput.xInput != 0) //When the player is pressing left or right on the ground.
        {
            
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, playerInput.xInput * playerInput.moveSpeed, accelerationCap ), rig.linearVelocity.y);
            playerInput.xInputMemory = playerInput.xInput;
            playerInput.directionalMemory = playerInput.xInput;

        }
        /*else if(playerInput.groundedCheck()) //When the player is not pressing left or right on the ground.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, 0, deccelerationCap ), rig.linearVelocity.y);
            playerInput.xInputMemory = playerInput.xInput;
        }*/
        /*else if(playerInput.xInput == 0) //When the player is not pressing left or right in the air.
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

        }*/
        
    }

    public override void Exit()
    {
        
    }
}
