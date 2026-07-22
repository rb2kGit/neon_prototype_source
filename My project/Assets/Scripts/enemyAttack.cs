using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class enemyAttack : MonoBehaviour
{
    //Attack variables.
    private float attackRange;
    private bool isPlayerInRange;
    private Vector2 attackPoint;
    private float attackType;

    //Player variables.
    private playerController playerController;
    private Vector2 playerPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<playerController>();
        attackRange = 2;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = playerController.getPlayerPosition();
        checkRange();
    }

    private void checkRange()
    {
        if (Vector2.Distance(transform.position, playerPosition) < attackRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }
}
