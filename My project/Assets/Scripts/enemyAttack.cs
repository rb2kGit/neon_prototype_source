using System;
using System.Numerics;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class enemyAttack : MonoBehaviour
{
    //Enemy obect variables.
    [SerializeField] protected Transform attackPos;
    [SerializeField] protected basicEnemyController controllerScript;

    //Attack variables.
    private float attackRange;
    private bool isPlayerInRange;
    private Vector2 attackPoint;
    private float attackType;
    private float maxAttackDelay;
    private float attackTimer;
    private float maxAttackChargeUp;
    private float attackChargeUp;
    private bool chargingUp;
    public bool canAttack;
    [SerializeField] private SpriteRenderer attackSprite;
    [SerializeField] private float spriteLingerTime;
    private float lingerTimer;
    protected bool isLingering;

    //Player variables.
    private playerController playerController;
    private Vector2 playerPosition;

    //virtual function to be overriden for enemy attacks;
    public virtual void attack(){}

    //Awake is called first.
    void Awake()
    {
        //Initialize enemy attack variables.
        initVariables(2, 3, 0.35f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reaquire player information.
        playerPosition = playerController.getPlayerPosition();
        checkRange();

        //Calculate the enemy attack cooldown.
        if (attackTimer >= maxAttackDelay && !canAttack)
        {
            canAttack = true;
        }
        else if(attackTimer < maxAttackDelay && !canAttack)
        {
            attackTimer = attackTimer + Time.deltaTime;
        }

        //Check if the enemy can attack and is in the attack state;
        if (canAttack && controllerScript.checkAttackStance() && !chargingUp)
        {
            //Start the attack charge up.
            startAttackChargeUp();
        }

        //Calculate the enemy attack charge up time. Attack once charged up.
        if (attackChargeUp >= maxAttackChargeUp && canAttack && chargingUp)
        {
            chargingUp = false;
            attack();
        }
        else if(attackChargeUp < maxAttackChargeUp)
        {
            attackChargeUp = attackChargeUp + Time.deltaTime;
        }

        //Calculate the temporary sprite lingering timer.
        if (isLingering)
        {
            updateLingerTimer();
        }


    }

    /*public void attackDelayCounter()
    {
        if (attackTimer >= maxAttackDelay && !canAttack)
        {
            canAttack = true;
        }
        else if(attackTimer < maxAttackDelay && !canAttack)
        {
            attackTimer = attackTimer + Time.deltaTime;
        }
    }*/

    private void startAttackChargeUp()
    {
        attackChargeUp = 0;
        chargingUp = true;
    }

    /*private void attackChargeUpCounter()
    {
        if (attackChargeUp >= maxAttackChargeUp && canAttack)
        {
            attack();
        }
        else if(attackChargeUp < maxAttackChargeUp)
        {
            attackChargeUp = attackChargeUp + Time.deltaTime;
        }
    }*/

    public void restartAttackDelay()
    {
        canAttack = false;
        attackTimer = 0;
    }

    private void checkRange()
    {
        if (Vector2.Distance(transform.position, playerPosition) < attackRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    //Initialization functions.
    private void initVariables(float newMaxAttackDelay, float newRange, float newMaxAttackChargeUP)
    {
        //Attack timers.
        maxAttackDelay = newMaxAttackDelay;
        attackTimer = 0;
        maxAttackChargeUp = newMaxAttackChargeUP;
        attackChargeUp = maxAttackChargeUp;
        canAttack = true;
        chargingUp = false;
        //Attack range.
        attackRange = newRange;
        //Attack sprite.
        attackSprite.enabled = false;
        lingerTimer = spriteLingerTime;
    }

    //Getter functions.
    public bool checkPlayerInRange()
    {
        return isPlayerInRange;
    }

    public bool checkAttackDelay()
    {
        return canAttack;
    }

    protected void startLingerTimer()
    {
        isLingering = true;
        attackSprite.enabled = true;
    }

    private void updateLingerTimer()
    {
        if (lingerTimer <= 0)
        {
            lingerTimer = spriteLingerTime;
            isLingering = false;
            attackSprite.enabled = false;
        }
        else
        {
            lingerTimer = lingerTimer - Time.deltaTime;
        }
    }

}
