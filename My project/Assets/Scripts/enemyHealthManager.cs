using System;
using UnityEngine;

public class enemyHealthManager : MonoBehaviour
{
    [SerializeField] private  basicEnemyController enemyController;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite damagedSprite;
    private Boolean isImmune;
    [SerializeField] private float maxImmuneTime;
    private float immuneTime;
    private float spawnTime;
    private spawnManager spawnManager;

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
