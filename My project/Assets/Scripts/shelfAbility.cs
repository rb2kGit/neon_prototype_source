using UnityEngine;

[CreateAssetMenu]
public class shelfAbility : abilityBase
{
    public GameObject shelfPrefab;
    public override void Activate(GameObject parent)
    {
        firePoint = GameObject.Find("FirePoint");
        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z);
        Instantiate(shelfPrefab, spawnPos, firePoint.transform.rotation);
    }
    
}
