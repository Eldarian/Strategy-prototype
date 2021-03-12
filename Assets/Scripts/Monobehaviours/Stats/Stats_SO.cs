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
        public int price;
    }

    #region Fields

    [SerializeField] Sprite portrait;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int price;

    [SerializeField] int baseDamage;
    [SerializeField] int currentLevel = 0;

    [SerializeField] Stats_SO unitStats;


    [SerializeField] LevelUp[] levelUps;

    #endregion

    #region Setters 

    public void InitializeHealth()
    {
        currentHealth = maxHealth;
    }

    public void SetLevel(int level)
    {
        while (currentLevel < level)
        {
            HandleLevelUp();
        }
    }

    #endregion

    #region Getters
    public int GetUnitPrice()
    {
        int price = unitStats.GetPrice();
        for (int i = 0; i < currentLevel; i++)
        {
            price += unitStats.GetLevelsData()[i].price;
        }
        return price;
    }
    public Sprite GetPortrait()
    {
        return portrait;
    }


    public int GetPrice()
    {
        return price;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetDamage()
    {
        return baseDamage;
    }

    public int GetLevel()
    {
        return currentLevel + 1;
    }

    public LevelUp[] GetLevelsData()
    {
        return levelUps;
    }
    #endregion

    #region Handlers
    public void ApplyHealth(int healthAmount)
    {
        Debug.Log("Healing");
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
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

    public void HandleLevelUp()
    {
        currentLevel += 1;

        maxHealth += levelUps[currentLevel - 1].maxHealth;
        InitializeHealth();
        baseDamage += levelUps[currentLevel - 1].baseDamage;
    }

    #endregion
}
