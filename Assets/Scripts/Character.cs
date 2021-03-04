using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Character : MonoBehaviour, ClickableObject
{
    #region Field Definitions
    public enum UnitState { Idle, Move, Chase, Attack }

    GameManager gameManager;
    UnitState state;
    public NavMeshAgent agent;
    Transform objective;

    LineRenderer selectionCircle;


    public virtual void Start()
    {
        print("Parent Start");
        gameManager = FindObjectOfType<GameManager>();
    }

    public virtual void Update()
    {

    }

    #endregion

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

    public void OnClickEvent()
    {
        if (!gameManager.isSelected(this)) 
        {
            gameManager.AddToSelected(this);
            if(selectionCircle == null)
            {
                selectionCircle = gameObject.DrawCircle(5f, 0.3f);
            } else
            {
                selectionCircle.enabled = true;
            }
            
        } 
        
    }

    public void Deselect()
    {
        selectionCircle.enabled = false;
    }
}
