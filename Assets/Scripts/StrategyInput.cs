using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrategyInput : MonoBehaviour
{
    private LayerMask clickablesLayer = 8;
    SelectionManager gameManager;
    public RectTransform selectionBox;
    Vector2 boxStartPos;

    private void Start()
    {
        gameManager = FindObjectOfType<SelectionManager>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //HandleSingleLeftClick();
            boxStartPos = Input.mousePosition;
            print(boxStartPos);
            //selectionBox.gameObject.SetActive(true);

        }

        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }

        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                print(hit.collider.gameObject);

            }
        }
    }

    // called when we are creating a selection box
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - boxStartPos.x;
        float height = curMousePos.y - boxStartPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = boxStartPos + new Vector2(width / 2, height / 2);

        UpdateSelection();

    }

    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

    }

    private void UpdateSelection()
    {
        gameManager.ClearSelection();
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);


        foreach (ClickableObject clickable in FindObjectsOfType<ClickableObject>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(clickable.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                clickable.Select();
            }

        }
    }





    private void HandleSingleLeftClick()
    {
        RaycastHit hit;
        //print("clicked");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            print(hit.collider.gameObject);
            if (hit.collider.gameObject.tag == "Terrain")
            {
                gameManager.ClearSelection();
            }
            else
            {
                var clickable = hit.collider.transform.parent.GetComponent<IClickable>();
                clickable.Select();
            }

        }
    }
}
