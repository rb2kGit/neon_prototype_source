using UnityEngine;

[CreateAssetMenu]
public class dropSign : abilityBase
{
    
    private Vector2 firepointPOS;
    private playerController playerController;
    private Camera cam;
    private Vector3 mousePos;
    [SerializeField] private GameObject prefab;
    private abilityHolder1 basicProjectileHolder;


    public override void Activate(GameObject parent)
    {
        //Initialize references for the base class.
        playerObject = GameObject.Find("Player");

        //Initialize the firing point object;
        firePoint = playerObject.transform.Find("FirePoint2").gameObject;
        //Initlialzie the character controller reference for flipping the player according to the mouse.
        playerController = playerObject.GetComponent<playerController>();
        //Initlialize the camera reference for converting the mouse cursor -> world space.
        cam = Camera.main;
        //Initialize and disable the basic projectile object. This ability will wait for the next mouse click.
        basicProjectileObject = GameObject.Find("PlayerAbilities").transform.Find("Ability1").gameObject;
        basicProjectileObject.SetActive(false);
        basicProjectileObjectUI = GameObject.Find("AbilityHotbar").transform.Find("Ability1").gameObject;
        basicProjectileObjectUI.SetActive(false);
        
        //Initialize status boolean for follow up input requirement.
        followUpInput = true;


        //Initialize reference to the abilityHolderScript;
        basicProjectileHolder = basicProjectileObject.GetComponent<abilityHolder1>();
    }

    public override void Deactivate(GameObject parent)
    {
        //Reacitvate the basic projectile ability again.
        basicProjectileObject.SetActive(true);
        basicProjectileObjectUI.SetActive(true);
        basicProjectileHolder.renableAbility();
    }

    public override void Fire(GameObject parent)
    {
        base.Fire(parent);

        //playerController playerController = playerObject.GetComponent<playerController>();
        //cam = Camera.main;
        //Set the mousePos variable with the postions of the mouse cursor.
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //turn the player towards the mouse aimer when firing.
        if (playerController.getFacingRight() && mousePos.x < playerObject.transform.position.x)
        {
            playerController.flipHandler();
        }
        else if (!playerController.getFacingRight() && mousePos.x > playerObject.transform.position.x)
        {
            playerController.flipHandler();
        }

        //Set the spawn position of the projectile.
        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, -1f);

        //Stop the player's movement.
        

        //Instantiate the projectile at the firepoint.
        Instantiate(prefab, spawnPos, firePoint.transform.rotation);
        //Instantiate(localPrefab, playerPos, playerObject.transform.rotation);

        //Put the abiilty holder in the disabled state.
        basicProjectileHolder.disableAbility();
        
        //Decrement the ability ammo.
        abilityAmmo -= 1;
    }
}
