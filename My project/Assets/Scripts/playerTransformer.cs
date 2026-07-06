using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class playerTransformer : MonoBehaviour
{
    [SerializeField] public GameObject baseProjectile;
    [SerializeField] private abilityInputManager inputManager;
    [SerializeField] private GameObject playerSpriteObject;
    [SerializeField] private GameObject defaultSprite;
    private GameObject currentSprite;
    [SerializeField] private GameObject rrSprite;
    private GameObject[] spriteObjects;
    [SerializeField] private GameObject abilityUIHotbar;
    [SerializeField] private playerSpriteManager spriteManager;
    [SerializeField] private uiAbilityBasicSpriteManager basicAbilitySpriteManager;
    private bool isPlayerTransformed;
    private int spriteCode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerTransformed = false;
        /*for (int i = 0; i < playerSpritesObject.transform.childCount; i++)
        {
            spriteObjects[i] = playerSpritesObject.transform.GetChild(i).gameObject; 
        }

        currentSprite = spriteObjects[0];*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateTieredAbility(abilityTHolder abilityHolder)
    {
        baseProjectile.SetActive(false);
        inputManager.aInput1 = false;
        abilityHolder.setActivation(true);
        isPlayerTransformed = true;

        setSprite(abilityHolder);
        swapAbilityImage(abilityHolder);
    }

    public void deactivateTieredAbility(abilityTHolder abilityHolder)
    {
        baseProjectile.SetActive(true);
        baseProjectile.GetComponent<abilityHolder>().putOnCooldown();
        abilityHolder.setActivation(false);
        isPlayerTransformed = false;

        resetSprite();
        resetAbilityImage();
    }

    public void setSprite(abilityTHolder abilityHolder)
    {
        SpriteRenderer playerSpriteRend = playerSpriteObject.GetComponent<SpriteRenderer>();
        playerSpriteRend.sprite = spriteManager.getPlayerSprite(abilityHolder.getAbilitySpriteCode());
    }

    public void swapAbilityImage(abilityTHolder abilityHolder)
    {
        basicAbilitySpriteManager.swapBasicAbilityImage(abilityHolder.ability.uiSpriteCode);
    }

    public void resetSprite()
    {
        SpriteRenderer playerSpriteRend = playerSpriteObject.GetComponent<SpriteRenderer>();
        playerSpriteRend.sprite = spriteManager.getDefaultSprite();
    }

    public void resetAbilityImage()
    {
        basicAbilitySpriteManager.resetAbilityImage();
    }

    public void setTransformed(bool value)
    {
        isPlayerTransformed = value;
    }

    public bool getTransformedFlag()
    {
        return isPlayerTransformed;
    }
}
