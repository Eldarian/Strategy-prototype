using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    #region Fields
    UnitFactory factory;
    [SerializeField] Character unitPrefab;
    Transform defaultObjective;
    Vector3 startPosition;
    [SerializeField] float spawnDistance;
    [SerializeField] float spawnDelay;
    ShopSystem shop;
    public Action CreateUnitAction;
    public Action UpgradeBarracksAction;

    #endregion

    #region Init
    public override void Start()
    {
        base.Start();
        shop = FindObjectOfType<ShopSystem>();
        factory = GetComponent<UnitFactory>();
        defaultObjective = new GameObject().transform;
        defaultObjective.transform.position = transform.position + transform.forward * spawnDistance * 3;
        startPosition = transform.position + transform.forward * spawnDistance;
        CreateUnitAction = CreateUnit;
        UpgradeBarracksAction = UpgradeBarracks;
    }

    #endregion

    #region Actions
    void CreateUnit()
    {
        if (moneyManager.SpendMoney(stats.GetUnitPrice()))
        {
            factory.OrderUnit(unitPrefab.gameObject, startPosition, defaultObjective, spawnDelay);
        } 
        else
        {
            Debug.Log("Not enough money");
        }
    }

    void UpgradeBarracks()
    {
        if (moneyManager.SpendMoney(stats.GetPrice()))
        {
            stats.HandleLevelUp();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    #endregion

    #region Selection
    public override void Select()
    {
        base.Select();
        shop.OnTakingUnit += CreateUnitAction;
        shop.OnUpgrade += UpgradeBarracksAction;
    }

    public override void Deselect()
    {
        base.Deselect();
        shop.OnTakingUnit -= CreateUnitAction;
        shop.OnUpgrade -= UpgradeBarracksAction;

    }
    #endregion
}
