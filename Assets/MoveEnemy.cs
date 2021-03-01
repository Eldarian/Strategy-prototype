using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    GameObject fortress;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fortress = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(fortress.transform.position);
        if(Vector3.Distance(transform.position, fortress.transform.position) < 7.5f)
        {
            agent.velocity = Vector3.zero;
        }
    }
}
