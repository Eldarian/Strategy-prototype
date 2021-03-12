using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableObject
{
    protected MoneyManager moneyManager;
    public override void Deselect()
    {
        base.Deselect();
    }

    public override void Select()
    {
        base.Select();
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        moneyManager = FindObjectOfType<MoneyManager>();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
