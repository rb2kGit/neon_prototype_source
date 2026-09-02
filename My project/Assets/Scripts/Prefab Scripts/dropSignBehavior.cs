using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class dropSignBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private BoxCollider2D thisCollider;
    private Vector2 signVelocity;
    private float maxLifespan;
    private float lifeTimer;
    private float knockbackFactor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signVelocity = rig.linearVelocity;
        maxLifespan = 10f;
        lifeTimer = maxLifespan;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the signVelocity;
        signVelocity = rig.linearVelocity;

        //Update the knockback factor;
        if (rig.linearVelocity.magnitude > 0f && knockbackFactor < (rig.linearVelocity.magnitude * 1.5f))
        {
            knockbackFactor = 1.5f * rig.linearVelocity.magnitude;
        }

        //Update the timer.
        if (lifeTimer <= 0f)
        {
            lifeTimer = maxLifespan;
            Destroy(gameObject);
        }
        else if (lifeTimer > 0f)
        {
            lifeTimer = lifeTimer - Time.deltaTime;
        }

        //Update the sign's collision rules based on if it is falling or not.
        if (signVelocity.y == 0)
        {
            thisCollider.excludeLayers = LayerMask.GetMask("Enemy", "Platform");
            Debug.Log("Exclude enemies.");
        }
        else
        {
            thisCollider.excludeLayers = LayerMask.GetMask("Platform");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        Rigidbody2D collidingRig = collidingObject.GetComponent<Rigidbody2D>();
        Vector2 collidingVelocity = collidingRig.linearVelocity;
        Collider2D enemyCollider = collidingObject.GetComponent<Collider2D>();    

        if (collidingObject.layer == 6)
        {
            Physics2D.IgnoreCollision(thisCollider, enemyCollider, true);
            collidingRig.AddForce(new Vector2(-collidingVelocity.x + knockbackFactor, 1 + (knockbackFactor /2)), ForceMode2D.Impulse);
        }
    }
}
