using System;
using UnityEngine;

public class jumpController : MonoBehaviour
{
    //Reference script variables.
    private playerController pControllerScript;
    private timerManager timerScript;

    //Player object variables.
    public Rigidbody2D playerRig;

    //Jump variables.
    public float jumpSpeed;
    public float downForce;
    private bool jumpInput;
    private bool jumpCut;
    private bool downForceInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pControllerScript = GetComponent<playerController>();
        timerScript = GetComponent<timerManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Capture jump input.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true; //Record jump as true to be used in jump execution method.
            timerScript.startJMemoryTimer(); //Start the jump memory timer in the timer manager script.
        }
        else if(Input.GetKeyUp(KeyCode.Space)) //Capture jump cut.
        {
            jumpCut = true;
        }

        //Capture down force input.
        if(Input.GetKeyDown(KeyCode.S))
        {
            downForceInput = true;
        }
    }

    void FixedUpdate()
    {
        jumpHandler();

        /* The jump will only be executed when the user is grounded, if the jump input has been pressed, within the remaining amount of jump memory time.
       if(jumpInput && timerScript.checkJumpMemory() && groundMemory > 0)
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

    private void jumpHandler()
    {
        if(jumpInput && timerScript.checkJumpMemory() && timerScript.checkGroundMemory())
       {
            playerRig.linearVelocity = new Vector2(playerRig.linearVelocity.x, jumpSpeed);
            jumpInput = false;
            jumpCut = false;
       }
       else if(jumpCut && playerRig.linearVelocity.y > 0 && !pControllerScript.groundedCheck()) //Apply jump cut velocity if space is not pressed and the y velocity is positive.
       {
            playerRig.linearVelocity = new Vector2(playerRig.linearVelocity.x, playerRig.linearVelocity.y * 0.50f);
            jumpCut = false;
       }
       else if(downForceInput && playerRig.linearVelocity.y <= 0 && !pControllerScript.groundedCheck()) //Apply downforce when the player is at the peak of the jump or falling.
       {
            playerRig.AddForce(transform.up * -downForce);
       }
       else
       {
            downForceInput = false; //Reset the downforce input before the player hits the ground.
       }
    }

    public void falsifyJumpInput()
    {
        jumpInput = false;
    }
}
