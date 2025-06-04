using UnityEngine;

public class abilityHolder3 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility3Input();

        abilityStateUpdate(abilityInput);

        inputManager.setAbility3Input(returnInput());
    }
}
