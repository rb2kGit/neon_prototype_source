using UnityEngine;

public class projectileBheavior : MonoBehaviour
{
    public float projectileSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void fixedUpdate()
    {
        transform.position = transform.position + -transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.Log("Destroy");
    }
}
