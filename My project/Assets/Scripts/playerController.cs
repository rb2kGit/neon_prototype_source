using UnityEngine;

public class playerController : MonoBehaviour
{
    //Character state variables.
    private bool isGrounded;
    public Vector2 playerPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        groundCheck(playerPosition, isGrounded);
    }

    public void groundCheck(Vector2 playerPosition, bool isGrounded)
    {
        //Initialize method variables.
        Vector2 position = playerPosition + new Vector2(0, -0.01f); //Added offset of -0.01 to Y axis. Need this to use overlap detection method.
        
        //Bookmark--> isGrounded = Physics2D.OverlapBox();

        

    }
}
