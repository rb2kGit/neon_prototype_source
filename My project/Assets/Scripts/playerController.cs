using UnityEngine;

public class playerController : MonoBehaviour
{
    //Level Variables
    public LayerMask groundLayer;

    //Raycasting variables.
    public Vector2 boxCastSize;
    public float boxCastDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }


    public bool groundedCheck()
    {
        //Raycast a box to detect a collision with the ground layer. 
        if(Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayer, 0, 0))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnDrawGizmos()
    {
        //Cahnge the gizmo colors to red.
        Gizmos.color = Color.red;
        
        //Draw a wire square that matches the location and size of the boxCast used for grounded checking.
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastDistance, boxCastSize);
    }

}
