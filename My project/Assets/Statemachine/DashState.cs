using UnityEngine;
using System.Collections;

public class DashState : State
{
    public override void Enter()
    {
        Debug.Log("Enter Dash");
    }

    public override void stateUpdate()
    {
        if (!playerInput.isDashing)
        {
            stateComplete = true;
        }
    }

    public override void Exit()
    {
        playerInput.canDash = true;
    }
}
