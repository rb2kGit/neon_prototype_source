using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class combinerParser
{
    [SerializeField]
    private int redInput, yellowInput, blueInput;
    private abilityTHolder selectedAbility;
    private combiner combiner;



    public void initiateCombinerParser(List<abilityHolder> combinerList, GameObject t2AbilityObject, GameObject t3AbiltyObject)
    {
        combiner = GameObject.Find("Player").GetComponentInChildren<combiner>();
        
        //Parse the combiner abilities into a color count. Then search a data set to determine which ability to activate.
        combinerCounter(combinerList);
        
        abilitySelector(t2AbilityObject, t3AbiltyObject);
        returnSelectedAbility();
        
    }

    public void combinerCounter(List<abilityHolder> combinerList)
    {
        foreach (abilityHolder ability in combinerList)
        {
            if (ability.ability.getColor() == 1)
            {
                redInput ++;
            }
            else if(ability.ability.getColor() == 2)
            {
                yellowInput ++;
            }
            else if (ability.ability.getColor() == 3)
            {
                blueInput ++;
            }
        }

    }

    public void abilitySelector(GameObject t2AbilityObject, GameObject t3AbilityObject)
    {
        
        abilityTHolder[] t2Abilities = t2AbilityObject.GetComponentsInChildren<abilityTHolder>();
        abilityTHolder[] t3Abilities = t3AbilityObject.GetComponentsInChildren<abilityTHolder>();

        for (int i = 0; i < t2Abilities.Length; i++)
        {
            if (t2Abilities[i].ability.redCount == redInput && t2Abilities[i].ability.yellowCount == yellowInput && t2Abilities[i].ability.blueCount == blueInput)
            {
                selectedAbility =  t2Abilities[i];
            }
            
        }

        for (int i = 0; i < t3Abilities.Length; i++)
        {
            if (t3Abilities[i].ability.redCount == redInput && t3Abilities[i].ability.yellowCount == yellowInput && t3Abilities[i].ability.blueCount == blueInput)
            {
                selectedAbility =  t3Abilities[i];
            }
            
        }
    }

    public void returnSelectedAbility()
    {
        redInput = 0;
        yellowInput = 0;
        blueInput = 0;

        combiner.setToBeActivated(selectedAbility);
        selectedAbility = null;
    }
}
