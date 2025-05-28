using UnityEngine;

public class platformBehavior : MonoBehaviour
{
    public GameObject playerObject;
    public playerController playerController;
    private float direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<playerController>();
        direction = playerController.directionalMemory;

        this.transform.position = new Vector3(playerObject.transform.position.x + (playerController.directionalMemory * 3f), playerObject.transform.position.y -1f, -2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
