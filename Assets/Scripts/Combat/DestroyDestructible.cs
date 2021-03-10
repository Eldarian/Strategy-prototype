using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDestructible : MonoBehaviour, IDestructible
{
    public void OnDestruction()
    {
        Destroy(gameObject);
    }
}
