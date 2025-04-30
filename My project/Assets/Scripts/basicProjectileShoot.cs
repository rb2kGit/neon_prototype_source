using UnityEngine;

public class basicProjectileShoot : MonoBehaviour
{
    public GameObject playerObject;
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
        Instantiate(projectilePrefab, firePoint.position, transform.rotation);
        Debug.Log("Shoot");
    }
}
