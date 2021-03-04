using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrategyInput : MonoBehaviour
{
    private LayerMask clickablesLayer = 8;
    SelectionManager selectionManager;
    public RectTransform selectionBox;
    Vector2 boxStartPos;

    public GameObject objectivePrefab;

    float clickTimer;
    float singleClickDuration = 0.3f;

    private void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clickTimer = 0;
            boxStartPos = Input.mousePosition;

        }

        if(Input.GetMouseButton(0))
        {
            clickTimer += Time.deltaTime;
            if(clickTimer > singleClickDuration)
            {
                UpdateSelectionBox(Input.mousePosition);
            }
            
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(clickTimer > singleClickDuration)
            {
                ReleaseSelectionBox();
            } else
            {
                HandleSingleLeftClick();

            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                
                foreach (IClickable clickable in selectionManager.GetSelected())
                {
                    if(typeof(Character).IsInstanceOfType(clickable) && !typeof(Enemy).IsInstanceOfType(clickable)) {
                        var ally = (Character) clickable;
                        if (hit.collider.CompareTag("Terrain"))
                        {
                            ally.SetObjective(null);
                            ally.MoveToPoint(hit.point, 5f);
                        }
                        else
                        {
                            ally.SetObjective(hit.collider.transform); 
                        }
                        
                    }
                }

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



    private void UpdateSelection()
    {
        selectionManager.ClearSelection();
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        var clickables = FindObjectsOfType<ClickableObject>();
        foreach (ClickableObject clickable in clickables)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(clickable.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                clickable.Select();
            }

        }
    }

    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        selectionManager.FilterSelection();

    }



    private void HandleSingleLeftClick()
    {
        RaycastHit hit;
        //print("clicked");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            selectionManager.ClearSelection();
            try
            {
                var clickable = hit.collider.transform.parent.GetComponent<IClickable>();
                clickable.Select();
            } catch (NullReferenceException e)
            {

            }
                
            

        }
    }
}
