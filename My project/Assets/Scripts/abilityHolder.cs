using UnityEngine;

public abstract class abilityHolder : MonoBehaviour
{
    public abilityBase ability;
    float cooldownTime;
    float activeTime;
    public playerController playerController;
    protected bool abilityInput;

    //Create a variable group that will help manage the state of the ability in this ability holder.
    public enum AbilityState
    {
        ready,
        active,
        cooldown,

    }

    AbilityState state = AbilityState.ready;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        abilityStateUpdate(abilityInput);
        /*switch (state)
        {
            case AbilityState.ready: //state = AbilityState.ready if the user presses the hotkey, call the activate function in the ability, move to the next state, otherwise do nothing.
                if (playerController.ability1Input && playerController.canDash)
                {
                    Debug.Log("Dash acivated.");
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active: //While state = Abilitystate.active countdown the ability timer, otherwise move to the next state and initialize cooldown time from the abilty.s

                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    activeTime = ability.activeTime;
                    cooldownTime = ability.cooldownTime;
                    state = AbilityState.cooldown;
                }

                break;
            case AbilityState.cooldown: //While state = abilitystate.cooldown countdown the cooldown timer otherwise move back to the ready state.
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    cooldownTime = ability.cooldownTime;
                    playerController.ability1Input = false;
                    state = AbilityState.ready;
                }
                break;
        }*/
    }

    public void abilityStateUpdate(bool abilityInput)
    {
        switch (state)
        {
            case AbilityState.ready: //state = AbilityState.ready if the user presses the hotkey, call the activate function in the ability, move to the next state, otherwise do nothing.
                if (abilityInput)
                {
                    Debug.Log("Ability acivated.");
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active: //While state = Abilitystate.active countdown the ability timer, otherwise move to the next state and initialize cooldown time from the abilty.s

                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.Deactivate(gameObject);
                    activeTime = ability.activeTime;
                    cooldownTime = ability.cooldownTime;
                    state = AbilityState.cooldown;
                }

                break;
            case AbilityState.cooldown: //While state = abilitystate.cooldown countdown the cooldown timer otherwise move back to the ready state.
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    cooldownTime = ability.cooldownTime;
                    state = AbilityState.ready;
                }
                break;
        }
    }

}
