using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class pShieldChild : MonoBehaviour
{
    [SerializeField] private int damageNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageNumber = 20;
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        pShieldBehavior pShieldScript = transform.parent.gameObject.GetComponent<pShieldBehavior>();

        collisionObject.GetComponent<enemyHealthManager>().damage(damageNumber);

        pShieldScript.removeMeFromList(gameObject);

        Destroy(gameObject);
    }
}
