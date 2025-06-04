using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class abilityInputManager : MonoBehaviour
{
    public bool aInput1, aInput2, aInput3, aInput4, aInput5, aInput6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkAbilityInput();
    }

    private void checkAbilityInput()
    {
        //Capture ability inputs.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            aInput1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            aInput2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            aInput3 = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            aInput4 = true;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            aInput5 = true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            aInput6 = true;
        }
    }

    //Getters for each ability slot.
    public bool getAbility1Input()
    {
        return aInput1;
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

    //Setters for each ability slot.
    public void setAbility1Input(bool abilityInput)
    {
        aInput1 = abilityInput;
    }
    public void setAbility2Input(bool abilityInput)
    {
        aInput2 = abilityInput;
    }
    public void setAbility3Input(bool abilityInput)
    {
        aInput3 = abilityInput;
    }
    public void setAbility4Input(bool abilityInput)
    {
        aInput4 = abilityInput;
    }
    public void setAbility5Input(bool abilityInput)
    {
        aInput5 = abilityInput;
    }
    public void setAbility6Input(bool abilityInput)
    {
        aInput6 = abilityInput;
    }
}
