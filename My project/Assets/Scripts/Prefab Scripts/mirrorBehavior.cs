using UnityEngine;

public class mirrorBehavior : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject thisObject;
    public playerController playerController;
    public Rigidbody2D rig;
    public float lifeSpan;
    private float direction;

    void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<playerController>();
        direction = playerController.directionalMemory;
        thisObject.transform.Rotate(0, 180, 0);
        rig.gravityScale = 0f;

        this.transform.position = new Vector3(playerObject.transform.position.x + (playerController.directionalMemory * 2f), playerObject.transform.position.y, -2f);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer();
        flipMirror();
        direction = playerController.directionalMemory;

        this.transform.position = new Vector3(playerObject.transform.position.x + (playerController.directionalMemory * 2f), playerObject.transform.position.y, -2f);
    }

    private void flipMirror()
    {
        float previousDirection = direction;
        float newDirection = playerController.directionalMemory;

        if (previousDirection != newDirection)
        {
            thisObject.transform.Rotate(0, 180, 0);
        }
    }

    private void lifeTimer()
    {
        if (lifeSpan > 0)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
