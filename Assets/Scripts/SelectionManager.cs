using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    List<ISelectable> selected = new List<ISelectable>();

    public void AddToSelected(List<ISelectable> multiple)
    {
        ClearSelection();
        if (multiple.Count > 1)
        {
            foreach (ISelectable clickable in multiple)
            {
                if (typeof(Character).IsInstanceOfType(clickable) && !typeof(Enemy).IsInstanceOfType(clickable))
                {
                    AddToSelected(clickable);
                }
            }
        }
        else if (multiple.Count == 1)
        {
            AddToSelected(multiple[0]);
        }
    }

    public void AddToSelected(ISelectable clickable)
    {
        //CheckSelectionTypes(clickable);
        selected.Add(clickable);
        print(selected.Count);
    }

    public void FilterSelection()
    {
        if (selected.Count > 1)
        {
            List<ISelectable> deselected = new List<ISelectable>();
            foreach (ISelectable clickable in selected)
            {
                if (typeof(Building).IsInstanceOfType(clickable) || typeof(Enemy).IsInstanceOfType(clickable))
                {
                    clickable.Deselect();
                    deselected.Add(clickable);
                }
            }
            if (deselected.Count > 0)
            {
                selected.RemoveAll(i => deselected.Contains(i));
            }
        }


    }

    public bool isSelected(ISelectable clickable)
    {
        return selected.Contains(clickable);
    }

    public void RemoveFromSelection(ISelectable clickable)
    {
        if (selected.Contains(clickable))
        {
            selected.Remove(clickable);
        }
    }

    public void ClearSelection()
    {
        foreach (ISelectable clickable in selected)
        {
            clickable.Deselect();
        }
        selected = new List<ISelectable>();
    }

    public List<ISelectable> GetSelected()
    {
        return selected;
    }

}
    
