using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    List<IClickable> selected = new List<IClickable>();

    public void AddToSelected(List<IClickable> multiple)
    {
        ClearSelection();
        if (multiple.Count > 1)
        {
            foreach (IClickable clickable in multiple)
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

    public void AddToSelected(IClickable clickable)
    {
        //CheckSelectionTypes(clickable);
        selected.Add(clickable);
        print(selected.Count);
    }

    void RemoveFromSelected(IClickable clickable)
    {
        if(selected.Contains(clickable))
        {
            selected.Remove(clickable);
        }
    }

    public bool isSelected(IClickable clickable)
    {
        return selected.Contains(clickable);
    }

    public void ClearSelection()
    {
        foreach (IClickable clickable in selected)
        {
            clickable.Deselect();
        } 
        selected = new List<IClickable>();
    }



    
    
    
    
   
    
    
    void CheckSelectionTypes(IClickable clickable)
    {
        if(typeof(Building).IsInstanceOfType(clickable) || typeof(Enemy).IsInstanceOfType(clickable))
        {
            ClearSelection();
            return;
        }

        if(typeof(Character).IsInstanceOfType(clickable))
        {
            if(selected.Count > 0)
            {
                if (typeof(Building).IsInstanceOfType(selected[0]) || typeof(Enemy).IsInstanceOfType(selected[0])) 
                {
                    ClearSelection();
                }
            }
        }
        //Buildings and characters can't be selected in one time
        //Player can select multiple units or single building
    }
}
