using System;
using System.Collections.Generic;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class abilityTHolder : MonoBehaviour
{
    public abilityBase ability;
    public float cooldownTime;
    public float maxCooldown;
    private bool shotCooldown;
    public playerController playerController;
    protected bool abilityInput;

    public abilityInputManager inputManager;
    public GameObject abilityImageObject;
    public Image abilityImage;
    private float imageFill;

    [SerializeField] public uiAbility abilityUI;
    [SerializeField] private uiAbilityBasicSpriteManager uiBasicAbility;
    public bool activation;
    [SerializeField] public GameObject baseProjectile;
    [SerializeField] private playerTransformer playerTransformer;
    private bool isActive;

    //Create a variable group that will help manage the state of the ability in this ability holder.
    public enum AbilityState
    {
        inactive,
        active

    }

    AbilityState state = AbilityState.inactive;

    public void Start()
    {
        maxCooldown = ability.getMaxCooldown();
        cooldownTime = maxCooldown;
    }


    public void abilityStateUpdate(bool activation, bool abilityInput)
    {
        switch (state)
        {
            case AbilityState.inactive: //state = AbilityState.ready if the user presses the hotkey, call the activate function in the ability, move to the next state, otherwise do nothing.

                isActive = activation;
                
                if (isActive)
                {
                    Debug.Log("Activate");
                    ability.Activate(gameObject);
                    playerTransformer.setTransformed(true);
                    setShotCooldown(false);
                    state = AbilityState.active;
                    
                }

                break;
            case AbilityState.active: //While state = Abilitystate.active countdown the ability timer, otherwise move to the next state and initialize cooldown time from the abilty.s

                if (abilityInput && ability.abilityAmmo > 0 && !shotCooldown)
                {
                    ability.Fire(gameObject);
                    inputManager.setAbility1Input(false);
                    setShotCooldown(true);
                    
                    imageFill = 0f;
                    uiBasicAbility.basicAbilityImageFill(imageFill);
                }
                else if(shotCooldown && cooldownTime > 0)
                {
                    cooldownTime = cooldownTime - Time.deltaTime;

                    imageFill = 1f - (cooldownTime / maxCooldown);
                    uiBasicAbility.basicAbilityImageFill(imageFill);
                }
                else
                {
                    cooldownTime = maxCooldown;
                    setShotCooldown(false);

                    imageFill = 1f;
                    uiBasicAbility.basicAbilityImageFill(imageFill);
                }
                

                if(ability.getAbilityAmmo() <= 0 || !isActive)
                {
                    ability.Deactivate(gameObject);
                    ability.resetAbilityAmmo();
                    isActive = false;
                    playerTransformer.deactivateTieredAbility(this);
                    state = AbilityState.inactive;
                    inputManager.setAbility1Input(false);
                    
                }

                inputManager.setAbility1Input(false);

                break;
        }
    }

    public void setActivation(bool activated)
    {
        activation = activated;
        ability.resetAbilityAmmo();
    }

    public int getAbilitySpriteCode()
    {
        return ability.spriteCode;
    }

    public void setShotCooldown(bool status)
    {
        shotCooldown = status;
    }

    /*public bool returnInput()
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
    public virtual void resetCombinerInput(){}*/

}
