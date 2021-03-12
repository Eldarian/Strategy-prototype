using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats", order = 55)]
public class Stats_SO : ScriptableObject //TODO make inheritance for different object types
{
    [System.Serializable]
    public class LevelUp
    {
        public int maxHealth;
        public int baseDamage;
    }

    [SerializeField] Sprite portrait;
    public int maxHealth;
    public int currentHealth;

    [SerializeField] int baseDamage;
    [SerializeField] int currentLevel = 0;

    public LevelUp[] levelUps;

    public Sprite GetPortrait()
    {
        return portrait;
    }
    public void ApplyHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetDamage()
    {
        return baseDamage;
    }

    public bool TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
        return currentHealth == 0;
    }

    private void HandleDeath()
    {
        
    }

    public void HandleLevelUp()
    {
        currentLevel += 1;

        maxHealth += levelUps[currentLevel - 1].maxHealth;
        baseDamage += levelUps[currentLevel - 1].baseDamage;
    }

    public void SetLevel(int level)
    {
        while(currentLevel < level)
        {
            HandleLevelUp();
        }
    }

    public int GetLevel()
    {
        return currentLevel + 1;
    }
}
