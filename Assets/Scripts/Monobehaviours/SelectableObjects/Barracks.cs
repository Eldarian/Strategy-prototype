using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    UnitFactory factory;
    [SerializeField] Character unitPrefab;
    Transform defaultObjective;
    Vector3 startPosition;
    [SerializeField] float distance;
    [SerializeField] float delay;
    ShopSystem shop;
    public Action CreateUnitAction;
    public Action UpgradeBarracksAction;

    public override void Start()
    {
        base.Start();
        shop = FindObjectOfType<ShopSystem>();
        factory = GetComponent<UnitFactory>();
        defaultObjective = new GameObject().transform;
        defaultObjective.transform.position = transform.position + transform.forward * distance * 3;
        startPosition = transform.position + transform.forward * distance;
        CreateUnitAction = CreateUnit;
        UpgradeBarracksAction = UpgradeBarracks;
    }

    void CreateUnit()
    {
        if (moneyManager.SpendMoney(stats.GetUnitPrice()))
        {
            factory.OrderUnit(unitPrefab.gameObject, startPosition, defaultObjective, delay);
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

}
