using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFortress : MonoBehaviour
{
    public void OnDestruction()
    {
        Destroy(gameObject);
        Debug.Log("Game Over");
    }
}
