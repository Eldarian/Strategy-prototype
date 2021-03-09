using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition : ScriptableObject
{
    float minDamage;
    float maxDamage; 

    public Attack CreateAttack(Stats attackerStats)
    {
        var damage = attackerStats.GetDamage() + Random.Range(minDamage, maxDamage);
        return new Attack((int) damage);
    }
}
