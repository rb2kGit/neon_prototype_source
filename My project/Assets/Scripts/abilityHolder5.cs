using UnityEngine;

public class abilityHolder5 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility5Input();

        abilityStateUpdate(abilityInput);

        inputManager.setAbility5Input(returnInput());
    }

    public override void setCombinerInput()
    {
        base.setCombinerInput();
        inputManager.setCombinerInput5();
    }

    public override void resetCombinerInput()
    {
        base.resetCombinerInput();
        inputManager.resetCombinerInput5();
    }
}
