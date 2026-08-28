using System;
using System.Collections.Generic;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class abilityHolder : MonoBehaviour
{
    public abilityBase ability;
    public float cooldownTime;
    float maxCooldown;
    float activeTime;
    public playerController playerController;
    protected bool abilityInput;
    protected bool lClickInput;

    public abilityInputManager inputManager;
    public GameObject abilityImageObject;
    public Image abilityImage;

    [SerializeField]
    private bool prepped = false;
    [SerializeField] public uiAbility abilityUI;

    //Variable to indicate if the ability requires a folloq up input.
    protected bool awaitingFollowUpInput;
    protected bool cancelHold;
    protected bool enableAbility;

    //Create a variable group that will help manage the state of the ability in this ability holder.
    public enum AbilityState
    {
        ready,
        holding,
        active,
        cooldown,
        disabled,

    }

    protected AbilityState state = AbilityState.ready;

    void Awake()
    {
        
    }

    public void abilityStateUpdate(bool abilityInput)
    {
        switch (state)
        {
            case AbilityState.ready: //state = AbilityState.ready if the user presses the hotkey, call the activate function in the ability, move to the next state, otherwise do nothing.

                if (abilityInput && ability.followUpInput)
                {
                    ability.Activate(gameObject);
                    state = AbilityState.holding;
                    awaitingFollowUpInput = true;
                    abilityImage.fillAmount = 0f;
                }
                else if (abilityInput)
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                    inputManager.setAbility1Input(false);
                    
                }

                break;
            case AbilityState.holding:

                if (cancelHold)
                {
                    cancelHold = false;
                    state = AbilityState.ready;
                }
                else if (abilityInput && inputManager.getFollowUpInput())
                {
                    ability.Fire(gameObject);
                    state = AbilityState.active;
                    awaitingFollowUpInput = false;
                    inputManager.setAbility1Input(false);
                }

                break;
            case AbilityState.active: //While state = Abilitystate.active countdown the ability timer, otherwise move to the next state and initialize cooldown time from the abilty.s
                
                /*if (awaitingFollowUpInput)
                {
                    ability.Fire(gameObject);
                    ability.Deactivate(gameObject);
                    activeTime = ability.activeTime;
                    cooldownTime = ability.cooldownTime;
                    maxCooldown = ability.cooldownTime;
                    state = AbilityState.cooldown;
                    inputManager.setAbility1Input(false);
                    ability.followUpInput = false;
                }*/

                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                    abilityImage.fillAmount = 0f;
                }
                else
                {
                    ability.Deactivate(gameObject);
                    activeTime = ability.activeTime;
                    cooldownTime = ability.cooldownTime;
                    maxCooldown = ability.cooldownTime;
                    state = AbilityState.cooldown;
                }

                break;
            case AbilityState.cooldown: //While state = abilitystate.cooldown countdown the cooldown timer otherwise move back to the ready state.
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                    abilityImage.fillAmount = 1f - (cooldownTime / maxCooldown);
                }
                else
                {
                    cooldownTime = ability.cooldownTime;
                    state = AbilityState.ready;
                }

                break;
            case AbilityState.disabled:
                if (enableAbility)
                {
                    cooldownTime = ability.cooldownTime;
                    maxCooldown = ability.cooldownTime;
                    state = AbilityState.cooldown;
                }
                break;
        }
    }

    public bool returnInput()
    {
        abilityInput = false;
        return abilityInput;
    }

    public void prep()
    {
        prepped = true;
    }

    public void free()
    {
        prepped = false;

        if (cooldownTime < maxCooldown)
        {
            ability.Deactivate(gameObject);
            state = AbilityState.cooldown;
        }
    }

    public void putOnCooldown()
    {
        ability.Deactivate(gameObject);
        activeTime = ability.activeTime;
        cooldownTime = ability.cooldownTime;
        maxCooldown = ability.cooldownTime;
        state = AbilityState.cooldown;
    }

    public void disableAbility()
    {
        enableAbility = false;
        state = AbilityState.disabled;
    }

    public void renableAbility()
    {
        enableAbility = true;
    }

    public bool checkPrep()
    {
        return prepped;
    }

    public bool checkActive()
    {
        if (state == AbilityState.active)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkReady()
    {
        if (state == AbilityState.ready)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void setCombinerInput(){}
    public virtual void resetCombinerInput(){}

}
