using UnityEngine;

public class abilityHolder2 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility2Input();

        abilityStateUpdate(abilityInput, lClickInput);

        inputManager.setAbility2Input(returnInput());
    }

    public override void setCombinerInput()
    {
        base.setCombinerInput();
        inputManager.setCombinerInput2();
    }

    public override void resetCombinerInput()
    {
        base.resetCombinerInput();
        inputManager.resetCombinerInput2();
    }
}
