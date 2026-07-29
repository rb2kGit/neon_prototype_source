using UnityEngine;

public class playerHealthManager : MonoBehaviour
{
    [SerializeField] private int playerShields;
    [SerializeField] private int shieldHealth;
    private float maxRegenTimer;
    private float regenTimer;
    private int playerHitValue;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private playerCoroutines coroutines;
    [SerializeField] private shieldUI shieldUI;
    private int shieldUIIndex;
    [SerializeField] private gameplayManager gameplayManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHitValue = 0;
        maxRegenTimer = 2f;
        regenTimer = 0;
        shieldUIIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHitValue > 0 && playerShields <= 0)
        {
            gameplayManager.restartGame();
        }
        else if (playerHitValue > 0 && regenTimer >= maxRegenTimer) //If the player is hit but the regen timer has fully counted down. Restore the shield health.
        {
            Debug.Log("Shield Restored! " + playerShields + " remaining");

            regenTimer = 0;
            playerHitValue = 0;
            shieldHealth = 2;

            shieldUI.resetShieldFillImage(shieldUIIndex);
        }
        else if (playerHitValue > 2) //If the player takes hit with value greater than 3, then just straight up remove a shield.
        {
            Debug.Log("Crushing Hit! Shield Lost! " + playerShields + " shields remaining!");

            playerHitValue = 0;
            playerShields = playerShields - 1;
            regenTimer = 0;
            shieldHealth = 2;
            
            StartCoroutine(coroutines.immuneRecovery());

            shieldUI.disbaleShieldImage(shieldUIIndex);
            shieldUIIndex ++;
        }
        else if (playerHitValue > 1 && shieldHealth < 2 && regenTimer > 0) //If the player is hit more than once and the shieldHealth is less than 2 and the regen timer is counting down. Lose a shield and acitvate immunity.
        {
            Debug.Log("Shield Lost! " + playerShields + " remaining!");

            playerHitValue = 0;
            playerShields = playerShields - 1;
            regenTimer = 0;
            shieldHealth = 2;
            StartCoroutine(coroutines.immuneRecovery());

            shieldUI.disbaleShieldImage(shieldUIIndex);
            shieldUIIndex ++;

        }
        else if (playerHitValue > 0 && shieldHealth < 2)
        {
            regenTimer += Time.deltaTime;

            shieldUI.setShieldImageFill(shieldUIIndex);
        }
    }

    public void damagePlayer(int damage)
    {

        Debug.Log("PLAYER HIT!");

        if (!coroutines.getImmuneStatus() && playerHitValue > 0 && damage > 0 && damage < 300)
        {
            playerHitValue = 2;
        }
        else if (!coroutines.getImmuneStatus() && damage > 0 && damage < 300 )
        {
            playerHitValue = 1;
            shieldHealth -= 1;
        }
        else if (!coroutines.getImmuneStatus() && damage >= 300)
        {
            playerHitValue = 3;
        }
    }
}
