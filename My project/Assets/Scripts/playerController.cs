using UnityEngine;

public class playerController : MonoBehaviour
{
    //Player object variables.
    public Rigidbody2D rig;
    public float moveSpeed;
    public float jumpSpeed;
    public float accelSpeed;
    public float decelSpeed;
    
    //Input Variables
    private float xInput;
    private float xInputMemory;
    private bool jumpInput;
    [SerializeField] private float jumpMemory;

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
        
        //Update input memory timers as of this frame.
        updateTimers();

    }

    void FixedUpdate() //Called a fixed amount of times per second.
    {
        moveHandler(); //Method to execute movement.
        jumpHandler(); //Method to execute jumps.
        
    }

    private void checkInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal"); //Record the left, center, and right inputs into a variable. Recorded as either -1, 0, 1;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true; //Record jump as true to be used in jump execution method.
            jumpMemory = 0.2f; //Set the jump memeory timer.
        }
        
    }

    private void moveHandler() //<---Left off. Figuring out how to dampen movement when jumping straight up.
    {
        //Initialize local variables.
        float currentVelocity = rig.linearVelocity.x; //Create a reference variable for the current velocity.
        float accelerationCap = accelSpeed * Time.fixedDeltaTime; //This varable will use the accelaration speed to create an accelartion cap in Mathf.MoveTowards, when combined with time.delta time.
        float deccelerationCap = decelSpeed * Time.fixedDeltaTime; //This varable will use the decceleration speed to create an decceleration cap in Mathf.MoveTowards, when combined with time.delta time.
        float airDampening = (accelSpeed * 0.6f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.

        if(groundedCheck() && xInput != 0) //When the player is not pressing left or right on the ground.
        {
            
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInput * moveSpeed, accelerationCap ), rig.linearVelocity.y);
            xInputMemory = xInput;
            
        }
        else if(groundedCheck()) //When the player is pressing left or right on the ground.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, 0, deccelerationCap ), rig.linearVelocity.y);
            xInputMemory = xInput;
        }
        else if(xInput == 0) //When the player is not pressing left or right in the air.
        {
            
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInputMemory * moveSpeed, airDampening ), rig.linearVelocity.y);
            
        }
        else //When the player is in the air.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInput * moveSpeed, airDampening ), rig.linearVelocity.y);
            xInputMemory = xInput;
        }
        Debug.Log(xInputMemory);
        /*else if(Mathf.Sign(rig.linearVelocity.x) != xInput)
        {
            rig.linearVelocity = new Vector2(xInput * airControlSpeed, rig.linearVelocity.y);
        }
        else if(rig.linearVelocity.x == 0)
        {
            rig.linearVelocity = new Vector2(xInput * airControlSpeed, rig.linearVelocity.y); //<------------------------ Pickup here. Figure out how player can change direction in mid air.
        }*/

        //horizontalVelocity  += xInput
        //horizontalVelocity *= Mathf.Pow(1f - horizontalDampTurning, moveSpeed * 10f);
        
    }

    private void jumpHandler()
    {
        //The jump will only be execute when the user is grounded, if the jump input has been pressed, within the remaining amount of jump memory time.
       if(groundedCheck() && jumpInput && jumpMemory > 0)
       {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, jumpSpeed);
            jumpInput = false;
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

    private void updateTimers()
    {
        //Decrement the jump input memory timer by time.delta time. If jump memory is 0, jumpInput becomes false.
        if(jumpMemory > 0)
        {
            jumpMemory -= Time.deltaTime;
        }
        else
        {
            jumpInput = false; //When the jump memory timer runs out. Set jump input to false.
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
