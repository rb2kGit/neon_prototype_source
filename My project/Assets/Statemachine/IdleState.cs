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
    }

    public override void Exit()
    {
        
    }
}
