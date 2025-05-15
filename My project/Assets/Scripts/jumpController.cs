using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class jumpController : MonoBehaviour
{
    //Reference script variables.
    private playerController playerController;
    private timerManager timerScript;

    //Player object variables.
    public Rigidbody2D playerRig;

    //Jump variables.
    public float jumpSpeed;
    public float downForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerController = GetComponent<playerController>();
        timerScript = GetComponent<timerManager>();
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

   public void jumpHandler()
   {
     
     if(playerController.jumpInput && timerScript.checkJumpMemory() && timerScript.checkGroundMemory())
     {
          playerRig.linearVelocity = new Vector2(playerRig.linearVelocity.x, jumpSpeed);
          playerController.jumpInput = false;
          playerController.jumpCut = false;
          timerScript.canCoyoteJump = false;
     }

    }

    public void falsifyJumpInput()
    {
        playerController.jumpInput = false;
    }
}
