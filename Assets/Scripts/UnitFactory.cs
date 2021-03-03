using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public Character SpawnUnit(GameObject prefab, Vector3 startPosition, Transform defaultObjective)
    {
        Character unit = Instantiate(prefab, startPosition, prefab.transform.rotation).GetComponent<Character>();
        return unit;
    }

}
