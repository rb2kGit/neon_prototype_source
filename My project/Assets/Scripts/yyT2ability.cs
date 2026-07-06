using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class yyT2Ability : abilityBase
{

    public override void Activate(GameObject parent)
    {
        Debug.Log("yellowShakalaka");
        /*firePoint = GameObject.Find("FirePoint");
        Vector3 spawnPos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, -1f);
        Instantiate(projectilePrefab, spawnPos, firePoint.transform.rotation);*/
    }
}
