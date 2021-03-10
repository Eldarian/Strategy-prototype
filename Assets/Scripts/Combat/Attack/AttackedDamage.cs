using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedDamage : MonoBehaviour, IAttackable
{
    private Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        Debug.LogFormat("Get {0} of damage from {1}", attack.GetDamage(), attacker);

        stats.TakeDamage(attack.GetDamage());
    }
}
