using UnityEngine;

public class abilityHolder6 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility6Input();

        abilityStateUpdate(abilityInput);

        inputManager.setAbility6Input(returnInput());
    }

    public override void setCombinerInput()
    {
        base.setCombinerInput();
        inputManager.setCombinerInput6();
    }
    
    public override void resetCombinerInput()
    {
        base.resetCombinerInput();
        inputManager.resetCombinerInput6();
    }
}
