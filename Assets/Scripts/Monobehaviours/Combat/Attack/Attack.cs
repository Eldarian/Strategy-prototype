using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private readonly int damage;
    private readonly GameObject attacker;

    public Attack (int _damage, GameObject _attacker)
    {
        damage = _damage;
        attacker = _attacker;
    }

    public int GetDamage()
    {
        return damage;
    }

    public GameObject GetAttacker()
    {
        return attacker;
    }
}
