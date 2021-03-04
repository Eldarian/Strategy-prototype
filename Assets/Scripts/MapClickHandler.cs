using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapClickHandler : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] bool placementMode = false;
    [SerializeField] bool walkMode = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            placementMode = !placementMode;
        }
    }

    private void OnMouseOver() //Basic mouseover
    {
        if (placementMode)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.point);

                Instantiate(unitPrefab, hit.point, Quaternion.identity);
            }
        }

        if (walkMode)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var point = ray.origin + (ray.direction);
            point.z = 0;
            if (Input.GetMouseButtonDown(1))
            {
                //SendToPoint(point);
            }
        }

    }

    /*private void SendToPoint(Vector3 point)
    {
        List<Character> selectedUnits = FindObjectsOfType<Character>().Where(unit => unit.isSelected).ToList();
        int length = selectedUnits.Count;
        int size = Mathf.RoundToInt(Mathf.Sqrt(length));
        float offset = -1 * (size-1) * 0.7f;

        List<Vector3> destinations = new List<Vector3>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                destinations.Add(new Vector3(i + offset, j + offset, 2));
            }
        }

        destinations.ForEach(vector => print(vector));
        var enumerator = destinations.GetEnumerator();
        enumerator.MoveNext();
        selectedUnits.ForEach(unit =>
        {
            //unit.Move(enumerator.Current+point);
            enumerator.MoveNext();
        });
    }
*/
}
