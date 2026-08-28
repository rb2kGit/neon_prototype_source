using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class abilityInputManager : MonoBehaviour
{
    public bool aInput1, aInput1Auto, aInput2, aInput3, aInput4, aInput5, aInput6, aInputX, aInputEsc, keyPressed, followUpFire;
    public bool[] combinerArray = new bool[5];
    [SerializeField] private playerTransformer playerTransformer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        checkAbilityInput();
    }

    private void checkAbilityInput()
    {
        while (Input.GetKey(KeyCode.LeftShift))
        {
            //Capture ability to load inputs.
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                aInput1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                setCombinerInput2();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                setCombinerInput3();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                setCombinerInput4();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                setCombinerInput5();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                setCombinerInput6();
            }
            
            return;
        }

        //Capture ability inputs.
        if (Input.GetKeyDown(KeyCode.Mouse0)  && !EventSystem.current.IsPointerOverGameObject())
        {
            aInput1 = true;
            keyPressed = true;
            followUpFire = true;

            Debug.Log(followUpFire);
        }
        else if(Input.GetKey(KeyCode.Mouse0)  && !EventSystem.current.IsPointerOverGameObject() && !playerTransformer.getTransformedFlag())
        {
            aInput1 = true;
            keyPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            aInput2 = true;
            keyPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            aInput3 = true;
            keyPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            aInput4 = true;
            keyPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            aInput5 = true;
            keyPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            aInput6 = true;
            keyPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            aInputX = true;
            keyPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            aInputEsc = true;
            keyPressed = true;
        }
    }

    //Getter for the keyPressed indicator.
    public bool getKeyPressed()
    {
        return keyPressed;
    }

    //Getter for the Escape Input.
    public bool getEscPressed()
    {
        return aInputEsc;
    }

    //Getters for each ability slot.
    public bool getAbility1Input()
    {
        if (aInput1Auto)
        {
            return aInput1Auto;
        }
        else
        {
            return aInput1;
        }
    }

    public bool getFollowUpInput()
    {
        return followUpFire;
    }
    public bool getAbility2Input()
    {
        return aInput2;
    }
    public bool getAbility3Input()
    {
        return aInput3;
    }
    public bool getAbility4Input()
    {
        return aInput4;
    }
    public bool getAbility5Input()
    {
        return aInput5;
    }
    public bool getAbility6Input()
    {
        return aInput6;
    }
    public bool getAbilityXInput()
    {
        return aInputX;
    }

    //Combiner Inputs
    public bool[] getCombinerInputs()
    {
        return combinerArray;
    }
    public void resetCombinerInputs()
    {
        combinerArray = new bool[5] {false, false, false, false, false};
    }

    //Setters for each ability slot.
    public void setAbility1Input(bool abilityInput)
    {
        aInput1 = abilityInput;
        aInput1Auto = abilityInput;
        followUpFire = abilityInput;
    }
    public void setAbility2Input(bool abilityInput)
    {
        aInput2 = abilityInput;
        keyPressed = false;
    }
    public void setAbility3Input(bool abilityInput)
    {
        aInput3 = abilityInput;
        keyPressed = false;
    }
    public void setAbility4Input(bool abilityInput)
    {
        aInput4 = abilityInput;
        keyPressed = false;
    }
    public void setAbility5Input(bool abilityInput)
    {
        aInput5 = abilityInput;
        keyPressed = false;
    }
    public void setAbility6Input(bool abilityInput)
    {
        aInput6 = abilityInput;
        keyPressed = false;
    }

    public void setAbilityXInput(bool abilityInput)
    {
        aInputX = abilityInput;
        keyPressed = false;
    }

    public void setAbilityEscInput(bool abilityInput)
    {
        aInputEsc = abilityInput;
        keyPressed = false;
    }

    public void setCombinerInput2()
    {
        combinerArray[0] = true;
    }
    public void setCombinerInput3()
    {
        combinerArray[1] = true;
    }
    public void setCombinerInput4()
    {
        combinerArray[2] = true;
    }
    public void setCombinerInput5()
    {
        combinerArray[3] = true;
    }
    public void setCombinerInput6()
    {
        combinerArray[4] = true;
    }

    public void resetCombinerInput2()
    {
        combinerArray[0] = false;
    }
    public void resetCombinerInput3()
    {
        combinerArray[1] = false;
    }
    public void resetCombinerInput4()
    {
        combinerArray[2] = false;
    }
    public void resetCombinerInput5()
    {
        combinerArray[3] = false;
    }
    public void resetCombinerInput6()
    {
        combinerArray[4] = false;
    }

}
