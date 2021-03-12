using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform objective;
    [SerializeField] int waveSize;
    int level;

    int waveNumber;
    UnitFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        factory = GetComponent<UnitFactory>();
    }

    public void CallWave(int _waveNumber, int _waveSize, int _enemiesLevel)
    {
        waveNumber = _waveNumber;
        waveSize = _waveSize;
        level = _enemiesLevel;
        SpawnWave();
    }

    private void SpawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
            var enemy = (Enemy)factory.GetUnitInstantly(prefab, transform.position + transform.forward * 15, null);
            enemy.waveNumber = waveNumber;
            enemy.GetStats().SetLevel(level);
        }
    }
}


/*GameObject character = factory.GetUnitInstantly(prefab, transform.position + transform.forward * 15, null).gameObject;
            Enemy enemy = character.GetComponent<Enemy>();
            enemy.waveNumber = waveNumber;
            Stats stats = character.GetComponent<Stats>();
            stats.SetLevel(level);*/