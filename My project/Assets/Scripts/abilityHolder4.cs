using UnityEngine;

public class abilityHolder4 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (awaitingFollowUpInput && inputManager.getKeyPressed() && !inputManager.getAbility4Input() || awaitingFollowUpInput && inputManager.getKeyPressed() && inputManager.getEscPressed())
        {
            cancelHold = true;
            abilityStateUpdate(abilityInput);
        }
        else if (awaitingFollowUpInput && inputManager.getKeyPressed() && inputManager.getEscPressed())
        {
            cancelHold = true;
            abilityStateUpdate(abilityInput);
            inputManager.setAbilityEscInput(false);
        }
        else if (awaitingFollowUpInput && inputManager.getAbility1Input())
        {
            abilityInput = inputManager.getAbility1Input();
            //inputManager.setAbility1Input(false);
            abilityStateUpdate(abilityInput);
        }
        else
        {
            abilityInput = inputManager.getAbility4Input();
            abilityStateUpdate(abilityInput);
        }

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
