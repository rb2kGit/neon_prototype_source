using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class rrrT3Ability : abilityBase
{

    public override void Activate(GameObject parent)
    {
        Debug.Log("RRR");
        
        /*
        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, -1f);
        Instantiate(projectilePrefab, spawnPos, firePoint.transform.rotation);*/
    }
}
