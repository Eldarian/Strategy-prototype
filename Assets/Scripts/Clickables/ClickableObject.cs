﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour, IClickable
{

    public SelectionManager selectionManager;
    public LineRenderer selectionCircle;
    public GameObject properties;

    // Start is called before the first frame update
    public virtual void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
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
            print(gameObject.name + "selected");
        }
    }
}