using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;
    public DamageType damageType;

    public void AttackPlayer(GameObject player)
    {
        // Deal damage to the player with a specified damage type
        player.GetComponent<PlayerHealth>().TakeDamage(damageType, damageAmount);
    }
}

/*public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;
    public DamageType damageType;

    public void AttackPlayer(IDamage player)
    {
        // Deal damage to the player with a specified damage type
        player.TakeDamage(damageAmount, damageType);
    }
}*/
