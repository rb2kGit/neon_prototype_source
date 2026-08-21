using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class enemyHealthManager : MonoBehaviour
{
    [SerializeField] private  basicEnemyController enemyController;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite damagedSprite;
    private Boolean isImmune, hasDOT, isSlowed, isRooted;
    private List<debuffs> debuffList = new List<debuffs>();
    [SerializeField] private float maxImmuneTime;
    private float immuneTime;
    private float spawnTime;
    private spawnManager spawnManager;
    [SerializeField] ParticleSystem redDOTFX;

    void Awake()
    {
        //Initialize the spawn manager.
        spawnManager = GameObject.Find("SpawnManager").GetComponent<spawnManager>();    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Time.time;
        immuneTime = 1f;

        initializeMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune && Time.time - spawnTime >= immuneTime)
        {
            isImmune = false;
        }

        if (hasDOT)
        {
            foreach(debuffs redDOT in debuffList)
            {
                //Debug.Log(redDOT.getDebuffType());
                redDOT.timer();
                hasDOT = redDOT.getDebuffStatus();
            }
        }
        else if (!hasDOT && debuffList.Count > 0)
        {
            debuffs debuff = debuffList.Find(dBuff => dBuff.getDebuffType() == 1); //Since only 1 of each debuff can be applied, we can search directly for the timer() override for each.
            debuff.removeMe();
            debuffList.Remove(debuff);
            redDOTFX.Stop();
        }

        healthCheck();
    }

    private void healthCheck()
    {
        if (currentHealth <= 0)
        {
            //Add this enemy to the spawnManager list.
            spawnManager.removeMeFromList(this.gameObject);
            
            Destroy(gameObject);
        }
        else if (currentHealth / maxHealth <= .5)
        {
            changeSprite();
        }
    }

    public void damage(int damage)
    {
        if (!isImmune)
        {
            //Play damage animation;
            currentHealth = currentHealth - damage;
            enemyController.stopMoveSpeed();
        }
    }

    public void debuff(debuffs debuff)
    {
        //Assign the debuff type to use.
        int debuffType = debuff.getDebuffType();

        switch (debuffType)
        {
            case 1:
                if (!hasDOT)
                {
                    hasDOT = true;
                    debuff.applyMe(this.gameObject);
                    debuff.triggerMe();
                    debuffList.Add(debuff);
                    redDOTFX.Play();
                }
                else
                {
                    foreach(debuffs redDOT in debuffList)
                    {
                        //Debug.Log(redDOT.getDebuffType());
                        redDOT.refreshMe();
                    }
                }
                break;
            case 2:
                isSlowed = true;
                debuffList.Add(debuff);
                break;
        }
    }

    public void clearDebuff(debuffs debuff)
    {
        //Assign the debuff type to use.
        int debuffType = debuff.getDebuffType();

        debuffList.Remove(debuff);
        Debug.Log(debuffList.Count);


        switch (debuffType)
        {
            case 1:
                hasDOT = false;
                Debug.Log(hasDOT);
                break;
            case 2:
                isSlowed = false;
                debuffList.Add(debuff);
                break;
        }
    }

    private void initializeMaxHealth()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
        isImmune = false;
    }

    private void changeSprite()
    {
        spriteRenderer.sprite = damagedSprite;
    }

}
