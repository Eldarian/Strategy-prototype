using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    SelectionService selectionService;
    public GameObject properties; //TODO make more clear

    private void Start()
    {
        selectionService = GetComponent<SelectionService>();
    }

    private void Update()
    {
        var selected = selectionService.GetSelected();
        if (selected.Count > 0)
        {
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
        Text health = properties.transform.GetChild(2).GetComponent<Text>(); //Temporary call
        health.text = "Health: " + selectable.GetStats().GetHealth();


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
    }
}
