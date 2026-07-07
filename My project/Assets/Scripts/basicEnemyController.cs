using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class basicEnemyController : MonoBehaviour
{
    //---------- VARBIALES ----------

    //Enemy Variables
    [SerializeField] private Transform thisTransform;
    [SerializeField] private Vector3 thisPosition;
    [SerializeField] private Rigidbody2D thisRig;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float reaquireDelay;
    [SerializeField] private float accelSpeed;
    private ThisState currentState;
    private Boolean isGrounded, isAirbourne, isAttacking, isMoving, isIdle, samePlatform;
    private Boolean flyer = false;
    private Boolean targetAcquired;
    private RaycastHit2D platformCollider;

    //Grounded Check Variables
    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private float boxCastDistance;
    [SerializeField] private Vector2 gapCheckBoxSize;
    [SerializeField] private Transform gapCheckerL;
    private Vector3 gapCheckerPositionL;
    [SerializeField] private Transform gapCheckerR;
    private Vector3 gapCheckerPositionR;

    //Level Variables
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private LayerMask platformLayer;

    //Player Variables
    private GameObject playerObject;
    private Vector3 playerPosition;
    private Vector3 playerDirection; //This will be used to find the direction of the player relative to the enemy.
    private RaycastHit2D playerPlatform;

    //State enumerator;
    private enum ThisState
    {
        Idle,
        Moving,
        Attacking,
        Airbourne,
    };

    //---------- VARBIALES ----------


    //---------- LOGIC ----------
    void Start()
    {
        playerObject = GameObject.Find("Player");
        currentState = ThisState.Idle;

        //Set the gap checker positions.
        gapCheckerPositionL.x = gapCheckerL.localPosition.x;
        gapCheckerPositionL.x = gapCheckerL.localPosition.y;
        gapCheckerPositionR.x = gapCheckerR.localPosition.x;
        gapCheckerPositionR.x = gapCheckerR.localPosition.y;


    }

    // Update is called once per frame
    void Update()
    {

        //Update the position, grounded status and the state of the enemy;
        updateMyPosition();
        groundedCheck();
        comparePlatforms();
        stateSelector();

        switch (currentState)
        {
            //Action tree switch.
            case ThisState.Idle:
                //Play idle animation.
                acquireTarget();
                if (targetAcquired)
                {
                    isMoving = true;
                }
                break;
            case ThisState.Moving:
                //Debug.Log("Moving");
                moveToPlayer();
                break;
            case ThisState.Attacking:
                //Attacking logic and animations
                break;
            case ThisState.Airbourne:
                //Airbourne 
                break;

        }

        
    }

    private void updateMyPosition()
    {
        //Update this enemy's position and the player position.
        thisPosition = thisTransform.position;
        //Update the known playerPosition;
        playerPosition = playerObject.transform.position;
        //Update the player direction.
        playerDirection.x = playerPosition.x - thisPosition.x;
        playerDirection = playerDirection.normalized;
        //Debug.Log(playerDirection);
    }

    private void stateSelector()
    {
        if (!isGrounded)
        {
            currentState = ThisState.Airbourne;
        }
        else if (isMoving && isGrounded)
        {
            currentState = ThisState.Moving;
        }
        else if (isAttacking && isGrounded)
        {
            currentState = ThisState.Attacking;
        }
        else
        {
            currentState = ThisState.Idle;
        }
    }

    private void groundedCheck()
    {

        platformCollider = Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayers, 0, 0 );
        //Debug.Log(platformCollider.collider);

        //Raycast a box to detect a collision with the ground or platform layer. 
        if(Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayers, 0, 0 ))
        {
            //Assign the collider.
            platformCollider = Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayers, 0, 0 );

            isGrounded = true;
            //platformCollider = 
            //Debug.Log("Grounded");
        }
        else
        {
            isGrounded = false;
            //Debug.Log("Airbourne");
        }
    }

    private void acquireTarget()
    {
        targetAcquired = true;
    }

    private void moveToPlayer() //<------------------------------- Make the basic enemy "go on patrol" when it cannot reach the player.
    {
        //Initialize local variables.
        float currentVelocity = thisRig.linearVelocity.x; //Create a reference variable for the current velocity.
        float accelerationCap = accelSpeed * Time.fixedDeltaTime; //This varable will use the accelaration speed to create an accelartion cap in Mathf.MoveTowards, when combined with time.delta time.

        //While the enemy is in the moving state start moving towards the player character if the gap checking boxcast does not hit ground on either side.
        if  (!Physics2D.BoxCast(new Vector3(gapCheckerL.position.x, gapCheckerL.position.y, 0), new Vector2(gapCheckBoxSize.x/2, gapCheckBoxSize.x/2), 0, -transform.up, boxCastDistance, groundLayers, 0, 0 ) || 
                !Physics2D.BoxCast(new Vector3(gapCheckerR.position.x, gapCheckerR.position.y, 0), new Vector2(gapCheckBoxSize.x/2, gapCheckBoxSize.x/2), 0, -transform.up, boxCastDistance, groundLayers, 0, 0 ))
        {
            thisRig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, -playerDirection.x * moveSpeed, accelerationCap ), thisRig.linearVelocity.y);

        }
        else if(samePlatform)
        {
            //Apply new position.
            thisRig.linearVelocity = new Vector2(Mathf.MoveTowards(currentVelocity, playerDirection.x * moveSpeed, accelerationCap ), thisRig.linearVelocity.y);
            //thisRig.linearVelocity = new Vector2(0, 0);
        }
    }

    private void getPlayerPlatform()
    {
        playerPlatform = playerObject.GetComponent<playerController>().getMyPlatform();
    }

    private bool comparePlatforms()
    {
        getPlayerPlatform();

        //Debug.Log(playerPlatform.collider);

        if (playerPlatform.collider == platformCollider.collider)
        {
            Debug.Log("Same Platform");
            samePlatform = true;
        }
        else
        {
            samePlatform = false;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        //Change the gizmo colors to red.
        Gizmos.color = Color.red;
        
        //Draw a wire square that matches the location and size of the boxCast used for grounded checking.
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastDistance, boxCastSize);

        //Draw a wire square that matches the location and size of the boxCast used for gap checking.
        Gizmos.DrawWireCube(new Vector3(gapCheckerL.position.x, gapCheckerL.position.y, 0), gapCheckBoxSize);
        Gizmos.DrawWireCube(new Vector3(gapCheckerR.position.x, gapCheckerR.position.y, 0), gapCheckBoxSize);
    }
}
