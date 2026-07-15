using UnityEngine;

public class enemyHealthManager : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeMaxHealth();   
    }

    // Update is called once per frame
    void Update()
    {
        healthCheck();
    }

private void healthCheck()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void damage(int damage)
    {
        currentHealth = currentHealth - damage;
    }

    private void initializeMaxHealth()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
    }
}
