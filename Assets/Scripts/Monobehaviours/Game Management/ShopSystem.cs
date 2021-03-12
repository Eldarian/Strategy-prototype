using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : SingletonBehaviour<ShopSystem>
{
    MoneyManager moneyManager;

    private void Awake()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }


    public event Action OnUpgrade;

    public event Action OnTakingUnit;

    public void TakingUnit()
    {
        OnTakingUnit();
    }

    public void Upgrade()
    {
        OnUpgrade();
    }
}
