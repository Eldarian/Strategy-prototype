using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private readonly int damage;

    public Attack (int _damage)
    {
        damage = _damage;
    }

    public int GetDamage()
    {
        return damage;
    }
}
