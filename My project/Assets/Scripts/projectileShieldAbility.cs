using UnityEngine;

[CreateAssetMenu]
public class projectileShieldAbility : abilityBase
{
    public GameObject pShieldPrefab;
    public GameObject spawnPoint;

    public override void Activate(GameObject parent)
    {
        spawnPoint = GameObject.Find("Player");
        Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -1f);
        Instantiate(pShieldPrefab, spawnPosition, spawnPoint.transform.rotation);
    }
}
