using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour, IDestructible
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnDestruction()
    {
        print("Destroyed");
        gameManager.OnEnemyDeath(GetComponent<Enemy>().waveNumber);
        Destroy(gameObject);
    }
}
