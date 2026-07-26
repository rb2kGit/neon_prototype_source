using System;
using System.Numerics;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class enemyAttack : MonoBehaviour
{

    [SerializeField] protected Transform attackPos;

    //Attack variables.
    private float attackRange;
    private bool isPlayerInRange;
    private Vector2 attackPoint;
    private float attackType;
    private float maxAttackDelay;
    private float attackTimer;
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
        initVariables(1, 3);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        attackDelayCounter();


        playerPosition = playerController.getPlayerPosition();
        checkRange();
        

        if (isPlayerInRange && canAttack)
        {
            attack();
        }

        if (isLingering)
        {
            updateLingerTimer();
        }


    }

    public void attackDelayCounter()
    {
        if (attackTimer >= maxAttackDelay && !canAttack)
        {
            canAttack = true;
        }
        else if(attackTimer < maxAttackDelay && !canAttack)
        {
            attackTimer = attackTimer + Time.deltaTime;
        }
    }

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
    private void initVariables(float newMaxAttackDelay, float newRange)
    {
        //Attack timers.
        maxAttackDelay = newMaxAttackDelay;
        attackTimer = maxAttackDelay;
        canAttack = true;
        //Attack range.
        attackRange = newRange;
        //Attack sprite.
        attackSprite.enabled = false;
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
