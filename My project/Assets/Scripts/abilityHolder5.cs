using UnityEngine;

public class abilityHolder5 : abilityHolder
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        abilityInput = playerController.ability5Input;

        abilityStateUpdate(abilityInput);
    }
}
