using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFortress : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnDestruction()
    {
        Destroy(gameObject);
        Debug.Log("Game Over");
        gameManager.OnGameEnd();
    }
}
