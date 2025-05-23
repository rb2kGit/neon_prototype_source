using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector2 = UnityEngine.Vector2;

public class playerCoroutines : MonoBehaviour
{
    //Player variables.
    public playerController playerController;
    public Rigidbody2D rig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startDash(float dashTime, float dashSpeed)
    {
        StartCoroutine(Dash(dashTime, dashSpeed));
    }

    private IEnumerator Dash(float dashTime, float dashSpeed)
    {
        //Initialize routine varaibles.
        float originalGravity = rig.gravityScale;
        UnityEngine.Vector2 originalVelocity = new Vector2(playerController.directionalMemory * playerController.moveSpeed, 0f); //Capture the current x velocity.

        //Set tracking variables.
        playerController.canDash = false;
        playerController.isDashing = true;

        //Set dash variables.
        rig.gravityScale = 0f;
        rig.linearVelocity = new Vector2(playerController.directionalMemory * dashSpeed, 0f); //Dash according to the last known direction.

        //Wait for a certain amount of seconds.
        yield return new WaitForSeconds(dashTime);

        //Reset dash and tracking variables.
        rig.gravityScale = originalGravity;
        rig.linearVelocity = originalVelocity; //return the characters velocity to what it was pre-dash.
        playerController.isDashing = false;
        playerController.ability1Input = false;
        playerController.canDash = true;
    }

    public IEnumerator bullCharge(Rigidbody2D bullRig, float direction, float bullSpeed, float chargeTime, GameObject bullObject)
    {
        yield return new WaitForSeconds(0.75f);

        bullRig.gravityScale = 0f;
        bullRig.linearVelocity = new Vector2(direction * bullSpeed, 0f);

        yield return new WaitForSeconds(chargeTime);

        bullRig.linearVelocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(1f);

        Destroy(bullObject);

    }
}
