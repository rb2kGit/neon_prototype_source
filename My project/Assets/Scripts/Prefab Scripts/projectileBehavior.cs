using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class projectileBehavior : MonoBehaviour
{
    public Rigidbody2D rig;
    public GameObject playerObj;
    public Vector3 playerRight;
    public Vector3 mousePosition;
    public Vector3 direction;
    public Camera cam;
    public float projectileSpeed;
    public float lifeSpan;
    public Vector2 initialVelocity;
    [SerializeField] private int damageValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeDamgeValue();

        playerObj = GameObject.Find("Player");
        playerRight = playerObj.transform.right;
        
        cam = Camera.main;
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;

        
        //initialVelocity = new Vector2(playerRig.linearVelocity.x - mousePosition.x + projectileSpeed, mousePosition.y + projectileSpeed);
        initialVelocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;

    }

    // Update is called once per frame.
    void Update()
    {
        updateTimers();

        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
        
        //rig.linearVelocity = playerRight * Time.fixedDeltaTime * initialVelocity;
        rig.linearVelocity = initialVelocity * Time.fixedDeltaTime;
    }

    private void initializeDamgeValue()
    {
        damageValue = 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<enemyHealthManager>().damage(damageValue);
        }

        Destroy(gameObject);
    }

    private void updateTimers()
    {
        lifeSpan -= Time.deltaTime;
        
    }
}
