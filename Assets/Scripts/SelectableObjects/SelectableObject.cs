using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour, ISelectable
{

    SelectionService selectionManager;
    LineRenderer selectionCircle;
    protected Stats stats;

    // Start is called before the first frame update
    public virtual void Start()
    {
        selectionManager = FindObjectOfType<SelectionService>();
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }
    public virtual void Deselect()
    {
        
        if(selectionCircle != null)
        selectionCircle.enabled = false;
    }

    public virtual void Select()
    {
        if (!selectionManager.isSelected(this))
        {
            selectionManager.AddToSelected(this);
            if (selectionCircle == null)
            {
                selectionCircle = gameObject.DrawCircle(transform.GetChild(0).localScale.x, 0.5f);
            }
            else
            {
                selectionCircle.enabled = true;
            }
        }
    }

    private void OnDestroy()
    {
        selectionManager.RemoveFromSelection(this);
    }

    public virtual Stats GetStats()
    {
        return stats;
    }
}