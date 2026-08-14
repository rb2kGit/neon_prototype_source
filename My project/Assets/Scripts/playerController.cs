using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using System;

public class playerController : MonoBehaviour
{

    //State variables.
    State state;
    public IdleState idleState;
    public RunState runState;
    public AirbourneState airbourneState;
    public DashState dashState;

    //Player object variables.
    public Rigidbody2D rig;
    public float moveSpeed;
    public float accelSpeed;
    public float decelSpeed;
    public float downForce;
    private bool facingRight;
    public bool isDashing;
    public bool canDash;
    private bool isDroppingDown;
    private RaycastHit2D myPlatform;
    private GameObject myPlatformObject;
    
    //Input Variables
    public float xInput { get; private set; } //xInput will be a property to allow the states to use it.
    public float xInputMemory;
    public float directionalMemory;
    public bool dashInput;
    public bool jumpInput;
    public bool jumpCut;
    public bool downForceInput;
    public bool ability1Input;
    public bool ability2Input;
    public bool ability3Input;
    public bool ability4Input;
    public bool ability5Input;
    public bool ability6Input;

    //Attack Variables
    public float battackTimer; 
    private bool hasDrones;
    private bool replaceDrones;

    //Level Variables
    public LayerMask groundLayer;
    public LayerMask groundLayers;
    public LayerMask platformLayer;

    //Raycasting variables.
    public Vector2 boxCastSize;
    public float boxCastDistance;

    //Reference Scripts
    public timerManager timerScript;
    public jumpController jumpController;
    [SerializeField] private playerCoroutines coroutines;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timerScript = GetComponent<timerManager>();
    }
    
    void Start()
    {
        //Initialize reference variables that we are sending the states. We are passing "this" which means passing in this script, that will be used by the state to read player inputs.
        idleState.Setup(rig, timerScript, jumpController, this);
        runState.Setup(rig, timerScript, jumpController, this);
        airbourneState.Setup(rig, timerScript, jumpController, this);
        dashState.Setup(rig, timerScript, jumpController, this);
        state = idleState;

        //Initialize variables that need a value at the start of the game.
        facingRight = true;
        directionalMemory = 1;
        canDash = true;
        hasDrones = false;
        replaceDrones = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Update input memory timers= as of this frame.
        //updateTimers();

        //Return out of Update if the character is dashing to disable inputs and state seelction while dashing.
        if (isDashing)
        {
            return;
        }

        //Check the movement input as of this frame.
        checkInput();
        

        //Determine if a new state should be selected in state selection function. Override the current state if necessary.
        stateSelect();
        //Once a state has been selected or if the state is not yet complete. Use the update function of the state.
        state.stateUpdate();

    }

    private void checkInput()
    {

        //Capture horizontal input.
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal"); //Record the left, center, and right inputs into a variable. Recorded as either -1, 0, 1;
        if (Input.GetKeyDown(KeyCode.A) && facingRight)
        {
            flipHandler();
        }
        else if (Input.GetKeyDown(KeyCode.D) && !facingRight)
        {
            flipHandler();
        }

        //Capture jump input.
        if (Input.GetKeyDown(KeyCode.Space) && !isDroppingDown)
        {
            jumpInput = true; //Record jump as true to be used in jump execution method.
            timerScript.startJMemoryTimer(); //Start the jump memory timer in the timer manager script.
        }

        //Capture jump cut.
        if (Input.GetKeyUp(KeyCode.Space) && !groundedCheck()) //Capture jump cut.
        {
            jumpCut = true;
        }
        else
        {
            jumpCut = false;
        }

        //Capture platform jump down input.
        if (Input.GetKeyDown(KeyCode.S) && myPlatformObject != null && myPlatformObject.layer == 9)
        {
            isDroppingDown = true;
            StartCoroutine(coroutines.disablePlatformCollision(this.gameObject, myPlatformObject));
        }

        //Capture down force input.
        if (Input.GetKeyDown(KeyCode.S))
        {
            downForceInput = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            downForceInput = false;
        }
    }

    public bool groundedCheck()
    {
        //Raycast a box to detect a collision with the ground or platform layer. 
        if(Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayer, 0, 0 ) || Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, platformLayer, 0, 0 ))
        {
            myPlatform = Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayers, 0, 0 );
            timerScript.resetGMemoryTimer();

            return true;
        }
        else
        {
            return false;
        }

    }

    public Vector2 getPlayerPosition()
    {
        return transform.position;
    }

    public RaycastHit2D getMyPlatform()
    {
        return myPlatform;
    }

    public void flipHandler()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public bool getFacingRight()
    {
        return facingRight;
    }

    //State management functions.
    private void stateSelect()
    {
        State prevState = state; //Store the last state to enter stateSelect.

        //Statement to select and assign the state to state;

        if (isDashing)
        {
            state = dashState;
        }
        else if (!groundedCheck())
        {
            state = airbourneState;
        }
        else if (groundedCheck() && xInput != 0)
        {
            state = runState;
        }
        else if (groundedCheck() && xInput == 0)
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

    public bool checkDrones()
    {
        return hasDrones;
    }

    public void setDrones(bool value)
    {
        hasDrones = value;
    }

    public bool replaceDroneCheck()
    {
        return replaceDrones;
    }

    public void setReplaceDrones(bool value)
    {
        replaceDrones = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            isDroppingDown = false;
        }

        if (collision.gameObject.layer == 9)
        {
            myPlatformObject = collision.gameObject;
        } 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 9)
        {
            myPlatformObject = null;
        }
    }

}
