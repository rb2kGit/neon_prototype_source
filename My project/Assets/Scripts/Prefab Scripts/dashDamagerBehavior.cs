using UnityEngine;

public class dashDamagerBehavior : MonoBehaviour
{
    //Variables
    [SerializeField] private int damageNumber;
    [SerializeField] private float maxLifeSpan;
    [SerializeField] private debuffs debuff;
    private float lifeTimer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifeTimer = 0;
        debuff = new redDOT();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTimer >= maxLifeSpan)
        {
            //Reset the timer;
            lifeTimer = 0;

            //Destroy the game object.
            Destroy(gameObject);
        }
        else if (lifeTimer < maxLifeSpan)
        {
            //Increase the timer;
            lifeTimer += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        //Apply dot.
        debuff.applyMe(collisionObject);
        //collisionObject.GetComponent<enemyHealthManager>().debuff(debuffs debuff);

        //collisionObject.GetComponent<enemyHealthManager>().damage(damageNumber);
    }
}
