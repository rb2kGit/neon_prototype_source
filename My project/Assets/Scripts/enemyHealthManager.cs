using System;
using UnityEngine;

public class enemyHealthManager : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite damagedSprite;
    private Boolean isImmune;
    private float immuneTime;
    private float spawnTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Time.time;
        immuneTime = 1.0f;

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
        }
        else
        {
            Debug.Log("Immune");
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
