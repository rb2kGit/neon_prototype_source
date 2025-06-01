using UnityEngine;

public class abilityHolder6 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = playerController.ability6Input;

        abilityStateUpdate(abilityInput);
    }
}
