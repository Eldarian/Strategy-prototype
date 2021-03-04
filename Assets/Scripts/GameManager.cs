using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<ClickableObject> selected = new List<ClickableObject>();

    public void AddToSelected(ClickableObject clickable)
    {
        selected.Add(clickable);
        print(selected.Count);
    }

    public bool isSelected(ClickableObject clickable)
    {
        return selected.Contains(clickable);
    }

    public void ClearSelection()
    {
        foreach (ClickableObject clickable in selected)
        {
            clickable.Deselect();
        } 
        selected = new List<ClickableObject>();
    }
}
