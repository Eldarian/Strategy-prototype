using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    int currentWaveNumber;
    [SerializeField] int initialWealth;

    WaveSpawner waveSpawner; //TODO make a List variable and tune game for multiple enemy spawn points
    MoneyManager moneyManager;
    [SerializeField] List<WaveDefinition_SO> waveDefinitions;


    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        moneyManager.AddMoney(initialWealth);
        StartCoroutine(GameLoop());
        
    }

    int GetCurrentWaveNumber()
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
    IEnumerator GameLoop()
    {
        for (int i = 0; i < waveDefinitions.Count; i++)
        {
            yield return new WaitForSeconds(waveDefinitions[i].delayBeforeWave);
            waveSpawner.CallWave(i, waveDefinitions[i].waveSize, waveDefinitions[i].enemyLevel);
            yield return new WaitForSeconds(waveDefinitions[i].waveDuration);
        }
    }

}
