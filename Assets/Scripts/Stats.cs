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
        stats_SO.TakeDamage(damageAmount);
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
