using UnityEngine;

public class playerController : MonoBehaviour
{
    //Player object variables.
    public Rigidbody2D rig;
    public float moveSpeed;
    public float jumpSpeed;
    public float accelSpeed;
    public float decelSpeed;

    [SerializeField]
    private float groundMemory;
    
    //Input Variables
    private float xInput;
    private float xInputMemory;
    private bool jumpInput;
    [SerializeField] private float jumpMemory;

    //Level Variables
    public LayerMask groundLayer;
    public LayerMask platformLayer;

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
            jumpMemory = 0.15f; //Set the jump memory timer.
        }
        
    }

    private void moveHandler()
    {
        //Initialize local variables.
        float currentVelocity = rig.linearVelocity.x; //Create a reference variable for the current velocity.
        float accelerationCap = accelSpeed * Time.fixedDeltaTime; //This varable will use the accelaration speed to create an accelartion cap in Mathf.MoveTowards, when combined with time.delta time.
        float deccelerationCap = decelSpeed * Time.fixedDeltaTime; //This varable will use the decceleration speed to create an decceleration cap in Mathf.MoveTowards, when combined with time.delta time.
        float airDampening = (accelSpeed * 0.5f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.
        float stillAirDampening = (accelSpeed * 0.1f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.

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
        else if(rig.linearVelocity.x < 0.5 * moveSpeed && rig.linearVelocity.y > 0) //When the player is rising with a slow speed, heavily dampen x velocity. Will make standstill jumping feel realistic.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInputMemory * moveSpeed, stillAirDampening ), rig.linearVelocity.y);
            xInputMemory = xInput;

        }
        else //When the player is in the air.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInput * moveSpeed, airDampening ), rig.linearVelocity.y);
            xInputMemory = xInput;
        }
        
    }

    private void jumpHandler()
    {
        //The jump will only be executed when the user is grounded, if the jump input has been pressed, within the remaining amount of jump memory time.
       if(jumpInput && jumpMemory > 0 && groundMemory > 0)
       {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, jumpSpeed);
            jumpInput = false;
            jumpMemory = 0;
            groundMemory = 0;
       }

    }

    public bool groundedCheck()
    {
        //Raycast a box to detect a collision with the ground layer. 
        if(Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayer, 0, 0 ) || Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, platformLayer, 0, 0 ))
        {
            Debug.Log("Grounded");
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

        //Decrement the grounded memory timer by time.delta time.
        if(!groundedCheck() && groundMemory > 0)
        {
            groundMemory = Mathf.Clamp(groundMemory, 0f, 0.15f) - Time.deltaTime;
        }
        else if(groundedCheck() && groundMemory <= 0)
        { 
            groundMemory = 0.15f;
        }
        Debug.Log(groundMemory);
    }

    private void OnDrawGizmos()
    {
        //Change the gizmo colors to red.
        Gizmos.color = Color.red;
        
        //Draw a wire square that matches the location and size of the boxCast used for grounded checking.
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastDistance, boxCastSize);
    }

}
