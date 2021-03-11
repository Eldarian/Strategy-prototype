using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTaker : MonoBehaviour
{
    SelectionService service;

    private void Start()
    {
        service = FindObjectOfType<SelectionService>();
    }

    public void OnButtonClicked()
    {
        service.TakingUnit();
    }
}
