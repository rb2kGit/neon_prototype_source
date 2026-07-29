using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class slowguyAttack : enemyAttack
{
    [SerializeField] private int attackValue; 

    public override void attack()
    {
        base.attack();

        int layersToExclude = LayerMask.GetMask("Enemy", "Ground", "Platform", "SelfProjectiles");
        int layerMask = ~layersToExclude;

        Collider2D contactCollider = Physics2D.OverlapBox(attackPos.position, new Vector2 (2,3), 0f, layerMask);

        if (contactCollider.gameObject.layer == 3)
        {
           contactCollider.gameObject.GetComponent<playerHealthManager>().damagePlayer(attackValue);
        }
        //Debug.Log("Player hit at this positoin: " + playerToDamage.gameObject.transform.position);

        restartAttackDelay();
        startLingerTimer();
        
    }

    //Draw gizmo to see the box.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPos.position, new Vector2(2, 3)); 
    }



}
