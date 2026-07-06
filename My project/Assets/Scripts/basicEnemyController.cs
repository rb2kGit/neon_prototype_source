using System;
using UnityEngine;

public class basicEnemyController : MonoBehaviour
{
    //Enemy Variables
    [SerializeField] private Transform thisTransform;
    [SerializeField] private Vector3 thisPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float reaquireDelay;
    private enum ThisState
    {
        Idle,
        Moving,
        Attacking,
        Airbourne,
    };
    private ThisState currentState;
    private Boolean isGrounded, isAirbourne, isAttacking, isMoving, isIdle;
    private Boolean flyer = false;

    //Grounded Check Variables
    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private float boxCastDistance;

    //Level variables.
    //Level Variables
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;

    //Player Variables
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Vector3 playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = ThisState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the known playerPosition;
        playerPosition = playerObject.transform.position;

        //Update the position, grounded status and the state of the enemy;
        updateMyPosition();
        groundedCheck();
        stateSelector();

        
    }

    private void updateMyPosition()
    {
        thisPosition = thisTransform.position;
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
        //Raycast a box to detect a collision with the ground or platform layer. 
        if(Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, groundLayer, 0, 0 ) || Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, boxCastDistance, platformLayer, 0, 0 ))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            isGrounded = false;
            Debug.Log("Airbourne");
        }
    }
}
