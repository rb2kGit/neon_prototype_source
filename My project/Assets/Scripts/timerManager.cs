using Mono.Cecil;
using TMPro;
using UnityEngine;

public class timerManager : MonoBehaviour
{
    //Reference script variables.
    //playerController pControllerScript;
    jumpController jControllerScript;

    //Timer variables.
    private float jumpMemory; //Jump memory timer.
    public bool canMemoryJump; //Jump memory boolean.
    private float groundMemory; //Coyote time timer.
    public bool canCoyoteJump; //Coyote time boolean.

    void Awake()
    {
        //pControllerScript = GetComponent<playerController>();
        jControllerScript = GetComponent<jumpController>();

        jumpMemory = 0f;
        groundMemory = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdownJMemoryTimer(); //Countdown the jumpMemoryTimer;
        countdownGMemoryTimer(); //Countdown the coyote time timer;
        
    }

    //Jump memory methods.
    public void countdownJMemoryTimer()
    {
        if(jumpMemory > 0)
        {
            jumpMemory -= Time.deltaTime;
        }
        else
        {
            jumpMemory = 0;
            canMemoryJump = false;
            jControllerScript.falsifyJumpInput();
        }
    }

    public void startJMemoryTimer()
    {
        jumpMemory = 0.2f;
        canMemoryJump = true;
    }

    public bool checkJumpMemory()
    {
        return canMemoryJump;
    }

    //Coyote timer methods.
    public void countdownGMemoryTimer()
    {
        if(groundMemory > 0)
        {
            groundMemory = Mathf.Clamp(groundMemory, 0f, 0.15f) - Time.deltaTime; //Mathf.Clamp will stop the ground memory value to drop less than 0.
        }
        else
        {
            groundMemory = 0f;
            canCoyoteJump = false;
        }
    }

    public void resetGMemoryTimer()
    {
        groundMemory = 0.15f;
        canCoyoteJump = true;
    }

    public bool checkGroundMemory()
    {
        return canCoyoteJump;
    }

}
