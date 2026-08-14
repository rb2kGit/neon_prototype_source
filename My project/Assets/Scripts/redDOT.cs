using System;
using UnityEngine;

public class redDOT : debuffs
{
    private int damageAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the damage amount to be dealt every second.
        damageAmount = 5;
        //Set the debuff type.
        setDebuffType(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void applyMe(GameObject targetObject)
    {
        base.applyMe(targetObject);

        if (targetObject.layer == 6)
        {
            targetObject.GetComponent<enemyHealthManager>().debuff(this);
        }
    }
}
