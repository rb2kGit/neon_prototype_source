using UnityEngine;

public class abilityHolder3 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility3Input();

        abilityStateUpdate(abilityInput, lClickInput);

        inputManager.setAbility3Input(returnInput());
    }

    public override void setCombinerInput()
    {
        base.setCombinerInput();
        inputManager.setCombinerInput3();
    }

    public override void resetCombinerInput()
    {
        base.resetCombinerInput();
        inputManager.resetCombinerInput3();
    }
}
