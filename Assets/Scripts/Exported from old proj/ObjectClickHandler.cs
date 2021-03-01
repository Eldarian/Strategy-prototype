using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickHandler : MonoBehaviour
{
    SpriteRenderer selection;
    [SerializeField] SelectableObject objectScript;

    private void Awake()
    {
        selection = transform.Find("selection").GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        //print("over");
        if (Input.GetMouseButtonDown(0))
        {
            objectScript.isSelected = !objectScript.isSelected;
            selection.enabled = objectScript.isSelected;
        }
    }
}
