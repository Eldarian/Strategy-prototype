using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonBehaviour<UIController>
{
    SelectionService selectionService;
    MoneyManager moneyManager;
    public GameObject properties; //TODO make more clear
    public GameObject bigPortrait;

    Text name;
    Text health;
    Text strength;
    Text level;
    Text money;
    Button takeUnit;
    Button buildWarBarracks;
    Button buildRangerBarracks;
    Button buildFountain;
    Button upgrade;

    private void Awake()
    {
        selectionService = GetComponent<SelectionService>();
        moneyManager = GetComponent<MoneyManager>();
    }

    private void Start()
    {
        name = properties.transform.Find("Name Label").GetComponent<Text>();
        health = properties.transform.Find("Health Label").GetComponent<Text>();
        strength = properties.transform.Find("Strength Label").GetComponent<Text>();
        level = properties.transform.Find("Level Label").GetComponent<Text>();
        money = GameObject.Find("Money Label").GetComponent<Text>();

        buildWarBarracks = properties.transform.Find("WarBarracks Button").GetComponent<Button>();
        buildRangerBarracks = properties.transform.Find("RangerBarracks Button").GetComponent<Button>();
        buildFountain = properties.transform.Find("Fountain Button").GetComponent<Button>();
        takeUnit = properties.transform.Find("Take Unit Button").GetComponent<Button>();
        upgrade = properties.transform.Find("Upgrade Button").GetComponent<Button>();
    }

    private void Update()
    {
        DrawMoney();
        var selected = selectionService.GetSelected();
        if (selected.Count > 0)
        {
            properties.SetActive(true);
            bigPortrait.SetActive(true);
            DrawMainObjectProperties(selected[0]);
        } 
        else
        {
            OnDeselect();
        }
    }

    private void DrawMoney()
    {
        money.text = "Money: " + moneyManager.GetWealth();
    }

    public void DrawMainObjectProperties(ISelectable selectable)
    {
        DrawPortrait(selectable);
        DrawName(selectable);
        DrawHealth(selectable);
        DrawStrength(selectable);
        DrawLevel(selectable);

        //Enable buttons in dependence of object type
        if(typeof(Fortress).IsInstanceOfType(selectable))
        {
            buildRangerBarracks.gameObject.SetActive(true);
            buildFountain.gameObject.SetActive(true);
            buildWarBarracks.gameObject.SetActive(true);
        } 
        else
        {
            buildRangerBarracks.gameObject.SetActive(false);
            buildFountain.gameObject.SetActive(false);
            buildWarBarracks.gameObject.SetActive(false);
        }

        if(typeof(Barracks).IsInstanceOfType(selectable))
        {
            takeUnit.gameObject.SetActive(true);
            upgrade.gameObject.SetActive(true);
        } else
        {
            takeUnit.gameObject.SetActive(false);
            upgrade.gameObject.SetActive(false);
        }
    }

    private void DrawName(ISelectable selectable)
    {
        name.text = selectable.GetStats().gameObject.name; //TODO replace to normal naming
    }

    private void DrawLevel(ISelectable selectable)
    {
        level.text = "Level: " + selectable.GetStats().GetLevel();
    }

    private void DrawStrength(ISelectable selectable)
    {
        //Set and enable strength value if existing
        if (selectable.GetStats().GetDamage() > 0)
        {
            strength.gameObject.SetActive(true);
            strength.text = "Strength: " + selectable.GetStats().GetDamage();
        }
        else
        {
            strength.gameObject.SetActive(false);
        }
    }

    private void DrawHealth(ISelectable selectable)
    {
        //Set and enable health value if existing
        health.text = "Health: " + selectable.GetStats().GetHealth();
    }

    private void DrawPortrait(ISelectable selectable)
    {
        //Set portrait image
        bigPortrait.GetComponent<Image>().sprite = selectable.GetStats().GetPortrait();
    }

    public void DrawSelectedUnitsIcons(ISelectable[] selectables)
    {
        //TODO Set portraits and counts for selected units
    }

    public void OnDeselect()
    {
        //hide and set all properties to default
        properties.SetActive(false);
        bigPortrait.SetActive(false);
    }
}
