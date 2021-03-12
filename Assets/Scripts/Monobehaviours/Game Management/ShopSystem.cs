using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : SingletonBehaviour<ShopSystem>
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

    }


    public event Action OnUpgrade;

    public event Action OnTakingUnit;

    public event Action<GameObject> OnBuild;

    public void TakingUnit()
    {
        OnTakingUnit();
    }

    public void Upgrade()
    {
        OnUpgrade();
    }

    public void Build(GameObject building)
    {
        OnBuild(building);
    }
}
