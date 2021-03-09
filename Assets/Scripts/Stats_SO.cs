using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats", order = 55)]
public class Stats_SO : ScriptableObject //TODO make inheritance for different object types
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    [SerializeField] int damage;
    [SerializeField] int currentLevel = 0;

    public LevelUp[] levelUps;

    [System.Serializable]
    public class LevelUp
    {
        public int maxHealth;
        public int damage;
    }

    public void ApplyHealth(int healthAmount)
    {
        health += healthAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            health = 0;
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log("Here is corpse!");
        throw new NotImplementedException();
    }

    public void HandleLevelUp()
    {
        currentLevel += 1;

        maxHealth += levelUps[currentLevel - 1].maxHealth;
        damage += levelUps[currentLevel - 1].damage;
    }
}
