using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour, ISelectable
{

    protected SelectionService selectionService;
    LineRenderer selectionCircle;
    protected Stats stats;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        selectionService = FindObjectOfType<SelectionService>();
        stats = gameObject.GetComponent<Stats>();
    }

    public virtual void Start()
    {

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
        if (!selectionService.isSelected(this))
        {
            selectionService.AddToSelected(this);
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
        selectionService.RemoveFromSelection(this);
    }

    public virtual Stats GetStats()
    {
        return stats;
    }
}