using UnityEngine;

public class testT2Holder : abilityTHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = inputManager.getAbility1Input();
        abilityStateUpdate(activation, abilityInput);
    }
}
