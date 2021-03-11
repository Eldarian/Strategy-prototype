using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    SelectionService selectionService;
    public GameObject properties; //TODO make more clear
    public GameObject bigPortrait;

    Text health;
    Text strength;
    Button TakeUnit;
    Button BuildWarBarracks;
    Button BuildArchBarracks;
    Button BuildFontain;

    private void Start()
    {
        selectionService = GetComponent<SelectionService>();

        health = properties.transform.Find("Health Label").GetComponent<Text>();
        strength = properties.transform.Find("Strength Label").GetComponent<Text>();

    }

    private void Update()
    {
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
    public void DrawMainObjectProperties(ISelectable selectable)
    {
        Debug.LogFormat("Drawing properties of {0}", selectable.ToString());

        bigPortrait.GetComponent<Image>().sprite = selectable.GetStats().GetPortrait();
        health.text = "Health: " + selectable.GetStats().GetHealth();
        if (selectable.GetStats().GetDamage() > 0)
        {
            strength.gameObject.SetActive(true);
            strength.text = "Strength: " + selectable.GetStats().GetDamage();
        } 
        else
        {
            strength.gameObject.SetActive(false);
        }
        //Set portrait image
        //Set and enable health value if existing
        //Set and enable strength value if existing
        //Enable buttons in dependence of object type
    }

    public void DrawSelectedUnitsIcons(ISelectable[] selectables)
    {
        //Set portraits and counts for selected units
    }

    public void OnDeselect()
    {
        //hide and set all properties to default
        properties.SetActive(false);
        bigPortrait.SetActive(false);
    }
}
