using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Attack", order = 56)]
public class AttackDefinition_SO : ScriptableObject
{
    public float minDamage;
    public float maxDamage; 

    public Attack CreateAttack(Stats attackerStats)
    {
        var damage = attackerStats.GetDamage() + Random.Range(minDamage, maxDamage);
        return new Attack((int) damage);
    }
}
