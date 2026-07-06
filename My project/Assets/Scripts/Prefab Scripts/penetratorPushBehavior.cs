using UnityEngine;

public class penetratorPushBehavior : MonoBehaviour
{
    private GameObject playerObject;
    private Vector3 playerPosition;
    [SerializeField] private CircleCollider2D thisCollider;
    [SerializeField] private float maxRadius;
    [SerializeField] private float expansionSpeed;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.Find("Player");

        playerPosition = playerObject.transform.position;
        transform.position = playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPosition;

        if (thisCollider.radius < maxRadius)
        {
            
            thisCollider.radius = thisCollider.radius + expansionSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        //collision.rigidbody.AddForce();
    }
}
