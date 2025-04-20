using UnityEngine;

public class playerController : MonoBehaviour
{
    //Player object variables.
    public Rigidbody2D rig;
    public float moveSpeed;
    public float jumpSpeed;
    //public float airControlSpeed;
    
    //Input Variables
    private float xInput;

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
        //Check the movement input as of this frame.
        checkInput();

    }

    void FixedUpdate() //Called a fixed amount of times per second.
    {
        moveHandler();
        
    }

    private void checkInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpHandler();
        }
    }

    private void moveHandler()
    {
        //Initialize local variables.
        float airControlSpeed = jumpSpeed * 0.5f;

        if(groundedCheck())
        {
            rig.linearVelocity = new Vector2(xInput * moveSpeed, rig.linearVelocity.y);
        }
        else if(Mathf.Sign(rig.linearVelocity.x) != xInput)
        {
            rig.linearVelocity = new Vector2(xInput * airControlSpeed, rig.linearVelocity.y);
        }
        else if(rig.linearVelocity.x == 0)
        {
            rig.linearVelocity = new Vector2(xInput * airControlSpeed, rig.linearVelocity.y); //<------------------------ Pickup here. Figure out how player can change direction in mid air.
        }
        
    }

    private void jumpHandler()
    {
        //Initialize local variables.

       if(groundedCheck())
       {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, jumpSpeed);
       }

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
        //Change the gizmo colors to red.
        Gizmos.color = Color.red;
        
        //Draw a wire square that matches the location and size of the boxCast used for grounded checking.
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastDistance, boxCastSize);
    }

}
