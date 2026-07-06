using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class basicProjectileAbility : abilityBase
{
    public GameObject projectilePrefab;

    public override void Activate(GameObject parent)
    {
        firePoint = GameObject.Find("FirePoint");
        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, -1f);
        Instantiate(projectilePrefab, spawnPos, firePoint.transform.rotation);
    }
}
