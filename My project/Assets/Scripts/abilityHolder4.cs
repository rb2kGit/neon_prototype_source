using UnityEngine;

public class abilityHolder4 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility4Input();

        abilityStateUpdate(abilityInput);

        inputManager.setAbility4Input(returnInput());
    }

    public override void setCombinerInput()
    {
        base.setCombinerInput();
        inputManager.setCombinerInput4();
    }

    public override void resetCombinerInput()
    {
        base.resetCombinerInput();
        inputManager.resetCombinerInput4();
    }
}
