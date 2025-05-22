using UnityEngine;
using System.Collections;

public class projectileBehavior : MonoBehaviour
{
    public Rigidbody2D rig;
    public GameObject playerObj;
    public Vector3 playerRight;
    public float projectileSpeed;
    public float lifeSpan;
    public Vector2 initialVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.Find("Player");
        playerRight = playerObj.transform.right;

        Rigidbody2D playerRig = playerObj.GetComponent<Rigidbody2D>();
        initialVelocity = new Vector2(playerRig.linearVelocity.x + projectileSpeed, 0);

    }

    // Update is called once per frame.
    void Update()
    {
        updateTimers();

        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
        
        rig.linearVelocity = playerRight * Time.fixedDeltaTime * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void updateTimers()
    {
        lifeSpan -= Time.deltaTime;
        
    }
}
