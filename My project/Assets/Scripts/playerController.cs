using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class playerController : MonoBehaviour
{

    //State variables.
    State state;
    public IdleState idleState;
    public RunState runState;
    public AirbourneState airbourneState;

    //Player object variables.
    public Rigidbody2D rig;
    public float moveSpeed;
    public float jumpSpeed;
    public float accelSpeed;
    public float decelSpeed;
    public float downForce;
    private float groundMemory;
    public Transform firePoint;
    private bool facingRight;
    
    //Jump variables.
    public bool jumpInput;
    public bool jumpCut;
    public bool downForceInput;

    [SerializeField] private float dashTime;
    private bool isDashing;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    private bool canDash;
    
    //Input Variables
    public float xInput { get; private set; } //xInput will be a property to allow the states to use it.
    public float xInputMemory;
    public float directionalMemory;
    private bool dashInput;

    //Attack Variables
    public float battackTimer; 

    //Level Variables
    public LayerMask groundLayer;
    public LayerMask platformLayer;

    //Raycasting variables.
    public Vector2 boxCastSize;
    public float boxCastDistance;

    //Reference Scripts
    public timerManager timerScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timerScript = GetComponent<timerManager>();
    }
    
    void Start()
    {
        //Initialize reference variables that we are sending the states. We are passing "this" which means passing in this script, that will be used by the state to read player inputs.
        idleState.Setup(rig, this);
        runState.Setup(rig, this);
        airbourneState.Setup(rig, this);
        state = idleState;

        //Initialize variables that need a value at the start of the game.
        facingRight = true;
        directionalMemory = 1;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check the movement input as of this frame.
        checkInput();
        
        //Update input memory timers= as of this frame.
        updateTimers();

        //Determine if a new state should be selected in state selection function. Override the current state if necessary.
        stateSelect();
        //Once a state has been selected or if the state is not yet complete. Use the update function of the state.
        state.stateUpdate();

    }

    void FixedUpdate() //Called a fixed amount of times per second.
    {
        //Return out of FixedUpdate immedately if the character is dashing to disable movement while dashing.
        if(isDashing) 
        {
            return;
        }

        dashHandler(); //Method to execute a dash.
        //moveHandler(); //Method to execute movement.
        //jumpHandler(); //Method to execute jumps.
        
    }

    private void checkInput()
    {

        //Capture horizontal input.
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal"); //Record the left, center, and right inputs into a variable. Recorded as either -1, 0, 1;
        if(Input.GetKeyDown(KeyCode.A) && facingRight)
        {
            flipHandler();
        }
        else if(Input.GetKeyDown(KeyCode.D) && !facingRight)
        {
            flipHandler();
        }

        //Capture jump input.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true; //Record jump as true to be used in jump execution method.
            timerScript.startJMemoryTimer(); //Start the jump memory timer in the timer manager script.
        }
        else if(Input.GetKeyUp(KeyCode.Space) && !groundedCheck()) //Capture jump cut.
        {
            jumpCut = true;
        }

        //Capture down force input.
        if(Input.GetKeyDown(KeyCode.S))
        {
            downForceInput = true;
        }

        //Capture dash input.
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            dashInput = true;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
        {
            attackHandler();
        }
        
    }

    /*private void moveHandler()
    {
        //Initialize local variables.
        float currentVelocity = rig.linearVelocity.x; //Create a reference variable for the current velocity.
        float accelerationCap = accelSpeed * Time.fixedDeltaTime; //This varable will use the accelaration speed to create an accelartion cap in Mathf.MoveTowards, when combined with time.delta time.
        float deccelerationCap = decelSpeed * Time.fixedDeltaTime; //This varable will use the decceleration speed to create an decceleration cap in Mathf.MoveTowards, when combined with time.delta time.
        float airDampening = (accelSpeed * 0.5f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.
        float stillAirDampening = (accelSpeed * 0.1f) * Time.fixedDeltaTime; //This varable will use the airDampening speed to create an airDempening cap in Mathf.MoveTowards, when combined with time.delta time.


        if(groundedCheck() && xInput != 0) //When the player is pressing left or right on the ground.
        {
            
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInput * moveSpeed, accelerationCap ), rig.linearVelocity.y);
            xInputMemory = xInput;
            directionalMemory = xInput;

        }
        else if(groundedCheck()) //When the player is not pressing left or right on the ground.
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
            directionalMemory = xInput;

        }
        else //When the player is in the air.
        {
            rig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, xInput * moveSpeed, airDampening ), rig.linearVelocity.y);
            xInputMemory = xInput;
            directionalMemory = xInput;

        }
        
    }*/

    private void jumpHandler()
    {
        
        /*//The jump will only be executed when the user is grounded, if the jump input has been pressed, within the remaining amount of jump memory time.
       if(jumpInput && timerScript.checkJumpMemory() && timerScript.checkGroundMemory())
       {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, jumpSpeed);
            jumpInput = false;
            jumpCut = false;
       }
       else if(jumpCut && rig.linearVelocity.y > 0 && !groundedCheck()) //Apply jump cut velocity if space is not pressed and the y velocity is positive.
       {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, rig.linearVelocity.y * 0.50f);
            jumpCut = false;
       }
       else if(downForceInput && rig.linearVelocity.y <= 0 && !groundedCheck()) //Apply downforce when the player is at the peak of the jump or falling.
       {
            rig.AddForce(transform.up * -downForce);
       }
       else
       {
            downForceInput = false; //Reset the downforce input before the player hits the ground.
       }*/

    }

    private void dashHandler() 
    {
        
        if(dashInput && canDash) //Start Coroutine if the player has pressed the dash button this frame AND if dash is not on cooldown in a prevous coroutine.
        {
            StartCoroutine(Dash());
        }
        else
        {
            dashInput = false; 
            //In the case that the If statement fails it means that either there is no dash input or dash is on cooldown.
            //This means that if dash is on cooldown, the dash input will fail. The player will have to use the key again.
        }
    }

    public bool groundedCheck()
    {
        //Raycast a box to detect a collision with the ground layer. 
        if(Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayer, 0, 0 ) || Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, platformLayer, 0, 0 ))
        {
            timerScript.resetGMemoryTimer();

            return true;
        }
        else
        {
            return false;
        }

    }

    private void updateTimers()
    {

        //Decrement the basic attack timer.
        if(battackTimer > 0)
        {
            battackTimer -= Time.deltaTime;
        }
        
    }

    private IEnumerator Dash()
    {
        //Initialize routine varaibles.
        float originalGravity = rig.gravityScale;
        Vector2 originalVelocity = new Vector2(directionalMemory * moveSpeed, 0f); //Capture the current x velocity.

        //Set tracking variables.
        canDash = false;
        isDashing = true;
        
        //Set dash variables.
        rig.gravityScale = 0f;
        rig.linearVelocity = new Vector2(directionalMemory * dashSpeed, 0f); //Dash according to the last known direction.

        //Wait for a certain amount of seconds.
        yield return new WaitForSeconds(dashTime);

        //Reset dash and tracking variables.
        rig.gravityScale = originalGravity;
        rig.linearVelocity = originalVelocity; //return the characters velocity to what it was pre-dash.
        isDashing = false;
        dashInput = false;

        yield return new WaitForSeconds(dashCooldown);

        //Reset tracking variables.
        canDash = true;
    }

    public void attackHandler()
    {
        if(battackTimer <= 0)
        {
            basicProjectileShoot shootScript = GetComponent<basicProjectileShoot>();
            shootScript.fireBasicProjectile();
            battackTimer = 0.5f;
        }
    }

    private void flipHandler()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //State management functions.
    private void stateSelect()
    {
        State prevState = state; //Store the last state to enter stateSelect.

        //Statement to select and assign the state to state;
        
        if(jumpInput || !groundedCheck())
        {
            state = airbourneState;
        }
        else if(groundedCheck() && xInput != 0 && !timerScript.checkJumpMemory())
        {
            state = runState;
        }
        else if(groundedCheck() && xInput == 0 && !timerScript.checkJumpMemory())
        {
            state = idleState;
        }

        //Check if the current State has changed since we entered this function. If it has override the previous state with the new one.
        if(prevState != state || prevState.stateComplete)
        {
            //Exit the previous state.
            prevState.Exit();
            
            //Initialize varaiables and eneter the new state.
            state.initStateVar();
            state.Enter();
        }

    }

    private void OnDrawGizmos()
    {
        //Change the gizmo colors to red.
        Gizmos.color = Color.red;
        
        //Draw a wire square that matches the location and size of the boxCast used for grounded checking.
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastDistance, boxCastSize);
    }

    public void falsifyJumpInput()
    {
        jumpInput = false;
    }

    /*public void falsifyJumpCut()
    {
        jumpCut = false;
    }*/

}
