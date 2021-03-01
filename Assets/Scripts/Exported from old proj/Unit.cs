using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : SelectableObject
{
    Vector3 destination;
    [SerializeField] float speed = 1;

    public void Move(Vector3 destination)
    {
        this.destination = destination;
        print(gameObject.name + " goes to " + destination);
        StartCoroutine(PerformMovement(destination));
    }

    IEnumerator PerformMovement(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            if (destination != this.destination)
            {
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
    } 
}
