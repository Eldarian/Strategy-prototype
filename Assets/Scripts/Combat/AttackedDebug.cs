using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedDebug : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, Attack attack)
    {
        Debug.LogFormat("Get {0} of damage from {1}", attack.GetDamage(), attacker);
    }
}
