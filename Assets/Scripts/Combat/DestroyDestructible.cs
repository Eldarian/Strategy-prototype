using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDestructible : MonoBehaviour, IDestructible
{
    public void OnDestruction()
    {
        if (GetComponent<Barracks>() != null)
        {
            GetComponent<Barracks>().Deselect();
        }
        Destroy(gameObject);
    }
}
