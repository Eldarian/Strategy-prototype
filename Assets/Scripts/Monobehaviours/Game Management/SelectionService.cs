using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SelectionService : SingletonBehaviour<SelectionService>
{
    public event Action OnTakingUnit;

    List<ISelectable> selected = new List<ISelectable>();

    public void TakingUnit()
    {
        OnTakingUnit();
    }


    public void AddToSelected(List<ISelectable> multiple)
    {
        ClearSelection();
        if (multiple.Count > 1)
        {
            foreach (ISelectable selectable in multiple)
            {
                if (typeof(Character).IsInstanceOfType(selectable) && !typeof(Enemy).IsInstanceOfType(selectable))
                {
                    AddToSelected(selectable);
                }
            }
        }
        else if (multiple.Count == 1)
        {
            AddToSelected(multiple[0]);
        }
    }

    public void AddToSelected(ISelectable selectable)
    {
        selected.Add(selectable);
    }

    public void FilterSelection()
    {
        if (selected.Count > 1)
        {
            List<ISelectable> deselected = new List<ISelectable>();
            foreach (ISelectable selectable in selected)
            {
                if (typeof(Building).IsInstanceOfType(selectable) || typeof(Enemy).IsInstanceOfType(selectable))
                {
                    selectable.Deselect();
                    deselected.Add(selectable);
                }
            }
            if (deselected.Count > 0)
            {
                selected.RemoveAll(i => deselected.Contains(i));
            }
        }
    }

    public bool isSelected(ISelectable selectable)
    {
        return selected.Contains(selectable);
    }

    public void RemoveFromSelection(ISelectable selectable)
    {
        if (selected.Contains(selectable))
        {
            selected.Remove(selectable);
        }
    }

    public void ClearSelection()
    {
        foreach (ISelectable selectable in selected)
        {
            selectable.Deselect();
        }
        selected = new List<ISelectable>();
    }

    public List<ISelectable> GetSelected()
    {
        return selected;
    }

}
    
