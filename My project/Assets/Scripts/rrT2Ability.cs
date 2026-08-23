using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class rrT2Ability : abilityBase
{

    private Vector2 firePointOrgPos;
    private Camera cam;
    private Vector3 mousePos;
    private Vector3 playerPos;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject localPrefab;

    public override void Activate(GameObject parent)
    {
        Debug.Log("RR");

        //Set the firepoint to a more intuitive position.
        playerObject = GameObject.Find("Player");

        firePoint = playerObject.transform.Find("FirePoint").gameObject;
        firePointOrgPos = new Vector2(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y);

        firePoint.transform.localPosition = new Vector2(1, 0);

        playerPos = playerObject.transform.position;

        abilityAmmo = 1;
        
    }

    public override void Deactivate(GameObject parent)
    {
        //Reset the firepoint back to its original position;
        firePoint.transform.localPosition = firePointOrgPos;
    }

    public override void Fire(GameObject parent)
    {
        //Debug.Log("Fire");

        playerController playerController = playerObject.GetComponent<playerController>();
        cam = Camera.main;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //turn the player towards the mouse aimer.
        if (playerController.getFacingRight() && mousePos.x < playerObject.transform.position.x)
        {
            playerController.flipHandler();
        }
        else if (!playerController.getFacingRight() && mousePos.x > playerObject.transform.position.x)
        {
            playerController.flipHandler();
        }

        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, -1f);
        Instantiate(prefab, spawnPos, firePoint.transform.rotation);
        Instantiate(localPrefab, playerPos, playerObject.transform.rotation);
        
        abilityAmmo -= 1;
    }
}
