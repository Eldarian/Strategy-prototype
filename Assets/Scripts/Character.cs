using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum UnitState { Idle, Move, Chase, Attack }

    UnitState state;

    public NavMeshAgent agent;

    Transform objective;

    public virtual void Heal()
    {

    }

    public void MoveToPoint(Vector3 point, float stopDistance)
    {
        agent.SetDestination(point);
        if (Vector3.Distance(transform.position, point) < stopDistance)
        {
            agent.velocity = Vector3.zero;
        }
    }

    public void SetObjective(Transform _objective)
    {
        objective = _objective;
    } 


    
}
