using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public abstract class State : MonoBehaviour
{
    //This State class will contain virtual functions that all states that implement this class will have access to.

    //State management variables that all states that implement this class will have.
    public bool stateComplete { get; protected set; } //This variable is changed into a property that allows anything to "get" the value, but only classes that implement this state class to set the value (protected).
    protected float startTime; //Float to keep track of the time that a state has been active.
    public float time => Time.time - startTime; //This variable is changed into psuedo function, so that whenever it is checked it will perform the calculation that is defined by the lambda (=>) symbol.

    //State groundcheck variables.
    protected Vector2 boxCastSize;
    protected float boxCastDistance;
    protected LayerMask groundLayer;
    protected LayerMask platformLayer;

    //Player variables.
    protected Rigidbody2D rig;
    protected playerController playerInput;

    //State methods.
    public virtual void Enter(){} //Method when the state is started. Replaces start.
    public virtual void stateUpdate(){} //Method that gets called every frame. Replaces Update.
    public virtual void stateFixedUpdate(){} //Method that gets called a fixed amount of times for physics. Replaces fixedUpdated.
    public virtual void Exit(){} //Method when the state ends.

    //Setup method to setup reference variables that will be used in every state.
    public void Setup(Rigidbody2D rBody, playerController playerController)
    {
        rig = rBody; //The rigidbody from the player controller.
        playerInput = playerController; //We are passing the player controller script, but we will be using it to read the input.
    }

    //Method to initialize and reset the is completeState variable and timer.
    public void initStateVar()
    {
        stateComplete = false;
        startTime = Time.time;
    } 
}
