using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrategyInput : MonoBehaviour
{

    #region Fields
    SelectionService selectionService;
    public RectTransform selectionBox;
    Vector2 boxStartPos;

    public GameObject buildingprefab;

    float clickTimer;
    float singleClickDuration = 0.3f;
    enum InputMode { Default, Build };
    InputMode mode = InputMode.Default;

    #endregion

    #region Init
    private void Start()
    {
        selectionService = FindObjectOfType<SelectionService>();
    }

    private void Update()
    {
        ManageInputModes();
    }

    private void ManageInputModes()
    {
        if (mode == InputMode.Default)
        {
            SelectUnits();
            SendUnits();
        }
        if (mode == InputMode.Build)
        {
            clickTimer += Time.deltaTime;
            if (clickTimer > singleClickDuration)
            {
                Build();
            }


            if (Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(1))
            {
                EnableDefaultMode();
            }
        }
    }

    #endregion

    #region Unit Selection
    private void SelectUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickTimer = 0;
            boxStartPos = Input.mousePosition;

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

            
        }
    }

    bool PointerOverGUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
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
        selectionService.ClearSelection();
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        var selectables = FindObjectsOfType<SelectableObject>();
        foreach (SelectableObject selectable in selectables)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(selectable.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectable.Select();
            }

        }
    }

    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        selectionService.FilterSelection();

    }

    private void HandleSingleLeftClick()
    {
        if(PointerOverGUI())
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            selectionService.ClearSelection();

            if (hit.collider.transform.parent != null && hit.collider.transform.parent.GetComponent<ISelectable>() != null)
            {
                var selectable = hit.collider.transform.parent.GetComponent<ISelectable>();
                selectable.Select();
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

                foreach (ISelectable selectable in selectionService.GetSelected())
                {
                    if (typeof(Character).IsInstanceOfType(selectable) && !typeof(Enemy).IsInstanceOfType(selectable))
                    {
                        var ally = (Character)selectable;
                        if (hit.collider.CompareTag("Terrain"))
                        {
                            ally.SetObjective(null);
                            ally.MoveToPoint(hit.point, 5f);
                        }
                        else
                        {
                            ally.SetObjective(hit.collider.transform.parent);
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
        if (Input.GetMouseButtonUp(0))
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
