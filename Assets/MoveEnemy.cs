using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveEnemy : Character
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    GameObject fortress;
    private float stopDistance = 7.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fortress = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPoint(fortress.transform.position, stopDistance);
    }
}
