using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform objective;
    [SerializeField] int waveSize;

    float time;
    int waveNumber;
    UnitFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        factory = GetComponent<UnitFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnRate)
        {
            time = 0;
            waveNumber++;
            SpawnWave();
        }

        
    }

    private void SpawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
            var enemy = (Enemy)factory.GetUnitInstantly(prefab, transform.position + transform.forward * 15, null);
            enemy.waveNumber = waveNumber; //TODO Create gameManager to manage waves, gold and game state
        }
    }
}
