using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class slowguyAttack : enemyAttack
{
    //Enemy variables.
    //[SerializeField] public Transform attackPos;

    public override void attack()
    {
        base.attack();

        Collider2D playerToDamage = Physics2D.OverlapBox(attackPos.position, new Vector2 (2,3), 0f);
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
