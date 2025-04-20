using UnityEngine;

public class cameraController : MonoBehaviour
{
    //Initialize camera variables.
    private Vector3 offset = new Vector3(0f, 0f, -10f); //Offset the z of the camera to ensure that it rencders.
    private Vector3 velocity = Vector3.zero;//Camera's velocity.
    private float smoothTime = 0.25f; //The time it takes for the camera to "catch up" to the player.

    //Initialize player variables.
    [SerializeField] private Transform playerTrans;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Initialize location variables.
        Vector3 targetPosition = playerTrans.position + offset;

        //Update that camera's positns using the SmoothDamp Unity method.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
