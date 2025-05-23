using UnityEngine;

public class bullObjectBehavior : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject thisObject;
    public Rigidbody2D rig;
    public playerCoroutines coroutines;
    public float speed;
    public float chargeTime;
    public float direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerObject = GameObject.Find("Player");
        coroutines = playerObject.GetComponent<playerCoroutines>();
        direction = playerObject.GetComponent<playerController>().directionalMemory;
        thisObject.transform.Rotate(0, 180, 0);

        this.transform.position = new Vector3(playerObject.transform.position.x + (playerObject.GetComponent<playerController>().directionalMemory * -3f), playerObject.transform.position.y + 1f, -2f);

        startRoutine();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startRoutine()
    {
        StartCoroutine(coroutines.bullCharge(rig, direction, speed, chargeTime, thisObject));
    }
}
