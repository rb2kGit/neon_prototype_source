using UnityEngine;

public class deathwall : MonoBehaviour
{
    //[SerializeField] BoxCollider2D thisCollider;
    [SerializeField] private gameplayManager gameplayManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int collidingLayerIndex = collision.gameObject.layer;

        if (collidingLayerIndex ==  3)
        {    
            Debug.Log("Destroy");
            gameplayManager.restartGame();
        } 
        //Destroy(collision.gameObject);
    }


}
