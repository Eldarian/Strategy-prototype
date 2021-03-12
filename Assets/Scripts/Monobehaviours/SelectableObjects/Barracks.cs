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
    public Action CreateUnitAction;

    public override void Start()
    {
        base.Start();
        factory = GetComponent<UnitFactory>();
        defaultObjective = new GameObject().transform;
        defaultObjective.transform.position = transform.position + transform.forward * distance * 3;
        startPosition = transform.position + transform.forward * distance;
        CreateUnitAction = CreateUnit;
    }

    void CreateUnit()
    {
        Debug.LogFormat("Taking unit in {0}", name);
        factory.OrderUnit(unitPrefab.gameObject, startPosition, defaultObjective, delay);
    }

    public override void Select()
    {
        base.Select();
        selectionService.OnTakingUnit += CreateUnitAction;
    }

    public override void Deselect()
    {
        base.Deselect();
        selectionService.OnTakingUnit -= CreateUnitAction;
    }

}
