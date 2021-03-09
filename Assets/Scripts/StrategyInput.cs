using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrategyInput : MonoBehaviour
{
    SelectionManager selectionManager;
    public RectTransform selectionBox;
    Vector2 boxStartPos;

    public GameObject buildingprefab;

    float clickTimer;
    float singleClickDuration = 0.3f;
    bool selecting = false;
    SelectableObject primarySelected = null;

    enum InputMode { Default, Build };
    InputMode mode = InputMode.Default;

    private void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    private void Update()
    {
        foreach (SelectableObject clickable in FindObjectsOfType<SelectableObject>())
        {
            clickable.properties.SetActive(false);
        }
        if (primarySelected != null)
        {
            primarySelected.properties.SetActive(true);
        }
        if (mode == InputMode.Default)
        {
            SelectUnits();
            SendUnits();
        }
        if (mode == InputMode.Build)
        {
            clickTimer += Time.deltaTime;
            if(clickTimer > singleClickDuration)
            {
                Build();
            }
            

            if(Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(1))
            {
                EnableDefaultMode();
            }
        }
    }

    #region Unit Selection
    private void SelectUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickTimer = 0;
            boxStartPos = Input.mousePosition;
            selecting = true;

        }

        if (Input.GetMouseButton(0))
        {
            clickTimer += Time.deltaTime;
            if (clickTimer > singleClickDuration)
            {
                UpdateSelectionBox(Input.mousePosition);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (clickTimer > singleClickDuration)
            {
                ReleaseSelectionBox();
            }
            else
            {
                HandleSingleLeftClick();
            }
            selecting = false;

        }
    }

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

        var clickables = FindObjectsOfType<SelectableObject>();
        foreach (SelectableObject clickable in clickables)
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            selectionManager.ClearSelection();
            try
            {
                var clickable = hit.collider.transform.parent.GetComponent<ISelectable>();
                clickable.Select();
            } catch (NullReferenceException e)
            {

            }
                
            

        }
    }

    #endregion

    #region Units Movement
    private void SendUnits()
    {
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {

                foreach (ISelectable clickable in selectionManager.GetSelected())
                {
                    if (typeof(Character).IsInstanceOfType(clickable) && !typeof(Enemy).IsInstanceOfType(clickable))
                    {
                        var ally = (Character)clickable;
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

    #endregion

    #region Buildings

    private void Build()
    {
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Instantiate(buildingprefab, hit.point, Quaternion.identity);
                EnableDefaultMode();
            }
        }
    }

    private void EnableDefaultMode()
    {
        mode = InputMode.Default;
    }

    public void EnableBuildMode(GameObject _buildingPrefab)
    {
        buildingprefab = _buildingPrefab;
        mode = InputMode.Build;
        clickTimer = 0;
    }
    #endregion
}
