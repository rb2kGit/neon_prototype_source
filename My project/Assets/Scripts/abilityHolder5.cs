using UnityEngine;

public class abilityHolder5 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (awaitingFollowUpInput && inputManager.getKeyPressed() && !inputManager.getAbility5Input() || awaitingFollowUpInput && inputManager.getKeyPressed() && inputManager.getEscPressed())
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
            abilityInput = inputManager.getAbility5Input();
            abilityStateUpdate(abilityInput);
        }

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
