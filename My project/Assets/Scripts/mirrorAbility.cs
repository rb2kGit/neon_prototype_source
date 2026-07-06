using UnityEngine;

[CreateAssetMenu]
public class mirrorAbility : abilityBase
{
    public GameObject mirrorPrefab;
    public playerController playerController;
    public jumpController jumpController;
    private bool burdened;
    private float originalMoveSpeed;
    private float originalJumpSpeed;

    public override void Activate(GameObject parent)
    {
        playerObject = GameObject.Find("Player");
        firePoint = GameObject.Find("FirePoint");
        playerController = playerObject.GetComponent<playerController>();
        jumpController = playerObject.GetComponent<jumpController>();
        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, -1f);
        Instantiate(mirrorPrefab, spawnPos, firePoint.transform.rotation);
        burdened = true;
        burden();
    }

    public override void Deactivate(GameObject parent)
    {
        burdened = false;
        burden();
    }

    private void burden()
    {
        if (burdened)
        {
            originalMoveSpeed = playerController.moveSpeed;
            originalJumpSpeed = jumpController.jumpSpeed;
            playerController.moveSpeed = playerController.moveSpeed * 0.5f;
            jumpController.jumpSpeed = jumpController.jumpSpeed * 0.75f;
        }
        else
        {
            playerController.moveSpeed = originalMoveSpeed;
            jumpController.jumpSpeed = originalJumpSpeed;
        }
    }
}
