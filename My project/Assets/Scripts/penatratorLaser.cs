using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class pentratorLaser : abilityBase
{
    private Vector2 firepointPOS;
    private playerController playerController;
    private Camera cam;
    private Vector3 mousePos;
    [SerializeField] private GameObject prefab;


    public override void Activate(GameObject parent)
    {
        Debug.Log("Activate penetrator.");
        //Initialize the firing point object;
        firePoint = GameObject.Find("Player").transform.Find("FirePoint").gameObject;
        //Initlialzie the character controller reference for flipping the player according to the mouse.
        playerController = GameObject.Find("Player").GetComponent<playerController>();
        //Initlialize the camera reference for converting the mouse cursor -> world space.
        cam = Camera.main;
        //Initialize and disable the basic projectile object. This ability will wait for the next mouse click.
        basicProjectileObject = GameObject.Find("PlayerAbilities").transform.Find("Ability1").gameObject;
        basicProjectileObject.SetActive(false);
        basicProjectileObjectUI = GameObject.Find("AbilityHotbar").transform.Find("Ability1").gameObject;
        basicProjectileObjectUI.SetActive(false);
        
        //Initialize status boolean for follow up input requirement.
        followUpInput = true;

        //Initialize references for the base class.
        playerObject = GameObject.Find("Player");
    }

    public override void Deactivate(GameObject parent)
    {
        //Reacitvate the basic projectile ability again.
        basicProjectileObject.SetActive(true);
        basicProjectileObjectUI.SetActive(true);
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

        //Instantiate the projectile at the firepoint.
        Instantiate(prefab, spawnPos, firePoint.transform.rotation);
        //Instantiate(localPrefab, playerPos, playerObject.transform.rotation);
        
        //Decrement the ability ammo.
        abilityAmmo -= 1;

    }
}
