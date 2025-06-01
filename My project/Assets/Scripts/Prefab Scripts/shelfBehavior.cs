using UnityEngine;

public class shelfBehavior : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject thisObject;
    public playerController playerController;
    public Rigidbody2D rig;
    public float lifeSpan;
    public float direction;
    

    void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<playerController>();
        direction = playerController.directionalMemory;
        //thisObject.transform.Rotate(0, 180, -1);
        //rig.gravityScale = 0f;

        this.transform.position = new Vector3(playerObject.transform.position.x + (playerController.directionalMemory * 2f), playerObject.transform.position.y - 2f, playerObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer();
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
