using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class pShieldBehavior : MonoBehaviour
{
    public Vector3 position;
    public Vector3 playerPosition;
    public float spinSpeed;
    public float lifeSpan;

    public GameObject child1;
    public GameObject child2;
    public GameObject child3;
    public GameObject child4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        this.transform.position = new Vector3(playerPosition.x, playerPosition.y, -1);
    }

    // Update is called once per frame
    void Update()
    {
        updateTimers();

        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
        playerPosition = GameObject.Find("Player").transform.position;

        this.transform.position = new Vector3(playerPosition.x, playerPosition.y, this.transform.position.z);
        this.transform.RotateAround(playerPosition, Vector3.forward, spinSpeed * Time.deltaTime);
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
