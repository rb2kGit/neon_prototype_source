using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class combiner : MonoBehaviour
{
    //Ability object variables
    [SerializeField] private GameObject t2AbilityObject;
    [SerializeField] private GameObject t3AbilityObject;
    [SerializeField] private abilityHolder abilityHolder;
    [SerializeField] private abilityHolder2 ability2;
    [SerializeField] private abilityHolder3 ability3;
    [SerializeField] private abilityHolder4 ability4;
    [SerializeField] private abilityHolder5 ability5;
    [SerializeField] private abilityHolder6 ability6;

    //Ability UI variables.
    [SerializeField] private abilityUIManager abilityUI;

    //Input Manager Variables
    [SerializeField] private abilityInputManager inputManager;
    private bool[] inputArray = new bool[5];
    private abilityHolder[] abilityArray;

    //Timer Manager
    [SerializeField] private timerManager timerManager;

    //Combiner Objects
    private List<abilityHolder> currentlyPrepped = new List<abilityHolder>();
    [SerializeField] uiCombiner combinerUI;

    //Parser
    [SerializeField] private combinerParser parser = new combinerParser();
    private abilityTHolder toBeActivated;
    [SerializeField] private uiCombiner uiCombiner;

    //Player Transformer
    [SerializeField] private playerTransformer playerTransformer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityArray = new abilityHolder[] {ability2, ability3, ability4, ability5, ability6};
    }

    // Update is called once per frame
    void Update()
    {
        //Check if a T2 abilty is to be activated.
        if (toBeActivated)
        {
            t2Activator();
        }

        if (areAbilitiesReady() && currentlyPrepped.Count() > 1)
        {
            combinerUI.activateReadyText();
        }
        else
        {
            combinerUI.disableReadyText();
        }

        //Check if the player is attempting to put abilities in the combiner.
        checkCombinerInput();

        //Check and prep abilities for the combiner. (Disable, and place in the combiner list)
        recheckPrep();

        //Check to see if the combiner has been activated.
        if (checkCombinerActivation() && areAbilitiesReady())
        {
            parser.initiateCombinerParser(currentlyPrepped, t2AbilityObject, t3AbilityObject);
            inputManager.setAbilityXInput(false);
        }
        else if(checkCombinerActivation() && !areAbilitiesReady())
        {
            Debug.Log("Abilites not ready!");
            inputManager.setAbilityXInput(false);
        }

        
        //Manage the combiner timer and free abilities if it has expired.
        if (!timerManager.checkCombinerTimer() && currentlyPrepped.Any())
        {
            for (int i = 0; i < abilityArray.Length; i++)
            {
                freeAbility(abilityArray[i]);
            }

            timerManager.stopCombinerCountdown(); //Stop the combiner countdown from counting.
        }
        else if (!currentlyPrepped.Any() && timerManager.checkCombinerTimer())
        {
            timerManager.stopCombinerCountdown(); //Stop the combiner countdown from counting.
        }
        
        inputManager.resetCombinerInputs(); //Reset the combiner hotkeys after each update.
    }

    //Functions
    private void checkCombinerInput(){
        bool[] tempArray = inputManager.getCombinerInputs();
        inputArray = (bool[])tempArray.Clone();

    }

    public void recheckPrep()
    {
        for (int i = 0; i < inputArray.Length; i++)
        {
            if (inputArray[i] && !abilityArray[i].checkPrep() && currentlyPrepped.Count < 3)
            {
                //Debug.Log("Prep");
                prepAbility(abilityArray[i]);

                timerManager.startCombinerCountdown(); //Manage the combiner countdown.

            }
            else if (inputArray[i] && abilityArray[i].checkPrep())
            {
                freeAbility(abilityArray[i]);
            }
        }
    }

    public void prepAbility(abilityHolder abilityHolder)
    {

        if (!abilityHolder.checkActive() && currentlyPrepped.Count < 3)
        {   
            //Debug.Log(abilityHolder.checkActive()); 
            abilityHolder.prep();
            currentlyPrepped.Add(abilityHolder);
            abilityUI.moveToCombiner(abilityHolder, uiCombiner);
        }
        else
        {
            //abilityUI.moveToPlace(abilityHolder);
        }
    }

    public void freeAbility(abilityHolder abilityHolder)
    {
        abilityHolder.free();
        currentlyPrepped.Remove(abilityHolder);
        timerManager.restartCombinerTimer();
        abilityUI.moveToPlace(abilityHolder);

    }

    private bool checkCombinerActivation()
    {
        return inputManager.getAbilityXInput();
    }

    public void setToBeActivated(abilityTHolder ability)
    {
        toBeActivated = ability;
    }

    private void t2Activator()
    {
    
        //Activate the T2 ability.
        playerTransformer.activateTieredAbility(toBeActivated);
        toBeActivated = null;

        //Reset the combiner timer and inputs.
        inputManager.resetCombinerInputs();
        timerManager.stopCombinerCountdown();

        //Put the combined abilities on cooldown.
        foreach (abilityHolder ability in currentlyPrepped)
        {
            ability.putOnCooldown();
        }
    }

    public bool isCombinerFull()
    {
        if (currentlyPrepped.Count < 3)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool areAbilitiesReady()
    {
        bool ready = true;

        foreach (abilityHolder ability in currentlyPrepped)
        {
            if (!ability.checkReady())
            {
                ready = false;
            }
        }

        return ready;

    }
}
