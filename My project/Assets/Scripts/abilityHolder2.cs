using UnityEngine;

public class abilityHolder2 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility2Input();

        abilityStateUpdate(abilityInput);

        inputManager.setAbility2Input(returnInput());
    }
}
