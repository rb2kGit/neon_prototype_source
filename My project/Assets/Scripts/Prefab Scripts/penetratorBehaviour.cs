using UnityEngine;

public class penetratorBehaviour : MonoBehaviour
{
    //This projectiles behavior.
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float projectileSpeed;
    private Vector3 direction;
    private Vector2 velocity;
    [SerializeField] private float lifeSpan;
    [SerializeField] private int maxPenValue;
    private int damageValue;
    private int penValue;

    //Player variables.
    [SerializeField] private GameObject firePoint;

    //Mouse variables.
    private Camera cam;
    private Vector3 mousePosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Camera and mouse variables.
        cam = Camera.main;
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //Set direction and rotation rotation.
        direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.right = direction;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        damageValue = 40;
        penValue = maxPenValue;

        velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Countdown life lifespan.
        lifeSpan -= Time.deltaTime;

        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        //Determine new position.
        rig.linearVelocity = velocity * Time.fixedDeltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;

        if (hitObject.layer == 6)
        {
            hitObject.GetComponent<enemyHealthManager>().damage(damageValue);
            penValue --;
        }

        if (penValue <= 0 || hitObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
