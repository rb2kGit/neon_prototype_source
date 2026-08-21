using System;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class redDOT : debuffs
{
    public int damageAmount;
    private GameObject theAfflicted;
    public int testNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        testNumber = UnityEngine.Random.Range(0, 10);
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cosntructor definition.
    public redDOT()
    {
        //Set the damage amount to be dealt every second.
        damageAmount = 2;
        //Set the debuff type.
        setDebuffType(1);

        maxDuration = 10f;
        remainingDuration = maxDuration;

        triggerTime = 1f;
        triggerTimer = 0f;

        //testNumber = UnityEngine.Random.Range(0, 10);
    }

    public override void timer()
    {
        //Manage the effect trigger timer.
        if (active && triggerTimer >= triggerTime)
        {
            triggerTimer = 0;
            triggerMe();
        }
        else if (active && triggerTimer < triggerTime)
        {
            triggerTimer += Time.deltaTime;//Count up the trigger timer.
        }

        if (active && remainingDuration <= 0)
        {
            active = false;
            remainingDuration = maxDuration;
            removeMe();
        }
        else if (active && remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;//Count down the duration timer.
        }

    }

    //How to apply the debuff?
    //Instantiate a scriptable object and pass it to the enemyHealthController.

    //How to affect the enemy.
    //During the timer trigger the effect.

    //How to remove the debuff?
    //Destroy the scriptable object.

    public override void applyMe(GameObject enemyObject)
    {
        base.applyMe(enemyObject);

        if (enemyObject.layer == 6)
        {
            theAfflicted = enemyObject.gameObject;
        }
    }

    public override void triggerMe()
    {
        theAfflicted.GetComponent<enemyHealthManager>().damage(damageAmount);
    }

    /*public override void refreshMe()
    {
        base.refreshMe();

        remainingDuration = maxDuration;
    }*/

    public override void removeMe()
    {
        if (!active)
        {
            Destroy(this);
        }
    }

    public override void refreshMe()
    {
        remainingDuration = maxDuration;
    }
}
