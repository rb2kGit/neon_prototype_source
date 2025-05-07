using UnityEngine;

public class RunState : State
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter()
    {
        Debug.Log("Enter Run");
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
    }

    public override void Exit()
    {
        
    }
}
