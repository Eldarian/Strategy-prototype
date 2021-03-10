using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] Stats_SO stats_SO;

    public int GetHealth()
    {
        return stats_SO.GetHealth();
    }

    public int GetDamage()
    {
        return stats_SO.GetDamage();
    }

    public void TakeDamage(int damageAmount)
    {
        print(gameObject.name + " " + GetHealth() + " hp");
        if(stats_SO.TakeDamage(damageAmount))
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        print("Try to die");
        if(GetComponent<IDestructible>() != null)
        {
            GetComponent<IDestructible>().OnDestruction();
        }
    }

    public void ApplyHealth(int healthAmount)
    {
        stats_SO.ApplyHealth(healthAmount);
    }

    public void HandleLevelUp()
    {
        stats_SO.HandleLevelUp();
    }
}
