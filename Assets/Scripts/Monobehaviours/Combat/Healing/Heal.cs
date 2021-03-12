using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IHealable
{
    public void OnHeal(int healPower)
    {
        GetComponent<Stats>().ApplyHealth(healPower);
    }
}
