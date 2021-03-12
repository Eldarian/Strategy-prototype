using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    #region Fields
    int currentWaveNumber;
    [SerializeField] int initialWealth;
    [SerializeField] List<WaveDefinition_SO> waveDefinitions;

    WaveSpawner waveSpawner; //TODO make a List variable and tune game for multiple enemy spawn points
    MoneyManager moneyManager;
    ShopSystem shop;
    StrategyInput input;
    public Action<GameObject> BuildAction;

    #endregion

    #region Game Init
    private void Awake()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        shop = FindObjectOfType<ShopSystem>();
        input = FindObjectOfType<StrategyInput>();
        BuildAction = Build;
        shop.OnBuild += BuildAction;
    }

    private void Start()
    {
        moneyManager.AddMoney(initialWealth);
        StartCoroutine(GameLoop());
    }
    IEnumerator GameLoop()
    {
        for (int i = 0; i < waveDefinitions.Count; i++)
        {
            yield return new WaitForSeconds(waveDefinitions[i].delayBeforeWave);
            waveSpawner.CallWave(i + 1, waveDefinitions[i].waveSize, waveDefinitions[i].enemyLevel);
            yield return new WaitForSeconds(waveDefinitions[i].waveDuration);
        }
    }

    #endregion

    #region Getters
    int GetCurrentWaveNumber() //for UI
    {
        return currentWaveNumber;
    }

    List<Enemy> GetWaveEnemiesList(int waveNumber)
    {
        var enemies = FindObjectsOfType<Enemy>();
        List<Enemy> waveEnemiesList = new List<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            if(enemy.waveNumber == waveNumber)
            {
                waveEnemiesList.Add(enemy);
            }
        }
        return waveEnemiesList;
    }


    private int GetWaveEnemiesCount(int waveNumber)
    {
        var enemies = FindObjectsOfType<Enemy>();
        int count = 0;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.waveNumber == waveNumber)
            {
                count++;
            }
        }
        return count;
    }

    private bool IsWaveDefeated(int waveNumber)
    {
        if (GetWaveEnemiesCount(waveNumber) == 0)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Action handlers
    public void OnEnemyDeath(int waveNumber)
    {
        if(IsWaveDefeated(waveNumber))
        {
            OnWaveDefeated(waveNumber);
        }
    }

    private void OnWaveDefeated(int waveNumber)
    {
        moneyManager.AddMoney(waveDefinitions[waveNumber].moneyReward);
    }

    public void OnGameEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Build(GameObject prefab)
    {
        var stats = prefab.GetComponent<Stats>();
        if (moneyManager.SpendMoney(stats.GetPrice()))
        {
            input.EnableBuildMode(prefab);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    #endregion
}
