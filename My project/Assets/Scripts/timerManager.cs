//using Microsoft.Unity.VisualStudio.Editor;
using Mono.Cecil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{
    //Reference script variables.
    //playerController pControllerScript;
    playerController playerScript;

    //Timer variables.
    private float jumpMemory; //Jump memory timer.
    public bool canMemoryJump; //Jump memory boolean.
    private float groundMemory; //Coyote time timer.
    public bool canCoyoteJump; //Coyote time boolean.
    [SerializeField]
    private float maxCombinerTime;
    private float combinerTimer;
    private bool combinerCountdown;
    [SerializeField] private Image combinerImage;

    void Awake()
    {
        //pControllerScript = GetComponent<playerController>();
        playerScript = GetComponent<playerController>();

        jumpMemory = 0f;
        groundMemory = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartCombinerTimer();
    }

    // Update is called once per frame
    void Update()
    {
        countdownJMemoryTimer(); //Countdown the jumpMemoryTimer;
        countdownGMemoryTimer(); //Countdown the coyote time timer;

        if (combinerCountdown)
        {
            countDownCombinerTimer(); //Coutndown the combiner timer;
        }
        
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
            playerScript.falsifyJumpInput();
        }
    }

    public void startJMemoryTimer()
    {
        jumpMemory = 0.1f;
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

    //Combiner Timer Methods
    public void startCombinerCountdown()
    {
        combinerTimer = maxCombinerTime;
        combinerCountdown = true;
    }

    public void stopCombinerCountdown()
    {
        combinerCountdown = false;
        restartCombinerTimer();
    }

    public void countDownCombinerTimer()
    {
        if (combinerTimer <= 0)
        {
            stopCombinerCountdown();
            combinerImage.fillAmount = 1f;
        }
        else
        {    
            combinerTimer = combinerTimer - Time.deltaTime;
            combinerImage.fillAmount = 0f + (combinerTimer / maxCombinerTime);
        }
    }

    public void restartCombinerTimer()
    {
        combinerTimer = maxCombinerTime;
        combinerImage.fillAmount = 1f;
    }

    public bool checkCombinerTimer()
    {
        if (combinerCountdown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
