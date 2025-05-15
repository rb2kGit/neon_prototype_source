using UnityEngine;

public class basicProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireBasicProjectile()
    {
        Vector3 spawnPos = new Vector3(firePoint.position.x, firePoint.position.y, -1f);
        Instantiate(projectilePrefab, spawnPos, transform.rotation);
    }
}
