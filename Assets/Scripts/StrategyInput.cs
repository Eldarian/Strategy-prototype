using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrategyInput : MonoBehaviour
{
    private LayerMask clickablesLayer = 8;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //print("clicked");
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                print(hit.collider.gameObject);
                if(hit.collider.gameObject.tag == "Terrain")
                {
                    gameManager.ClearSelection();
                } 
                else
                {
                    var clickable = hit.collider.transform.parent.GetComponent<ClickableObject>();
                    clickable.OnClickEvent();
                }
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                print(hit.collider.gameObject);

            }
        }
    }
}
