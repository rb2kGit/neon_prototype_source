using UnityEngine;

public class dropSignBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private BoxCollider2D thisCollider;
    private Vector2 signVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signVelocity = rig.linearVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the signVelocity;
        signVelocity = rig.linearVelocity;
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
            collidingRig.AddForce(new Vector2(-collidingVelocity.x + 30, 1 + 15), ForceMode2D.Impulse);
        }
    }
}
