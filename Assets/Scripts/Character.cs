using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Character : ClickableObject
{
    #region Field Definitions
    public enum UnitState { Idle, Move, Chase, Attack }

    UnitState state;
    public NavMeshAgent agent;
    Transform objective;
    public float stopDistance;


    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        base.Update();
        if(objective != null)
        {
            MoveToObjective();
        }
    }

    #endregion

    public virtual void Heal()
    {

    }

    public void MoveToPoint(Vector3 point, float _stopDistance)
    {
        agent.SetDestination(point);
        if (Vector3.Distance(transform.position, point) < _stopDistance)
        {
            agent.velocity = Vector3.zero;
        }
    }

    public void MoveToObjective()
    {
        MoveToPoint(objective.position, stopDistance);
    }

    public void SetObjective(Transform _objective)
    {
        objective = _objective;
    }

    public override void Select()
    {
        base.Select();
    }

    public override void Deselect()
    {
        base.Deselect();
    }

    /*public void Select()
    {
        if (!gameManager.isSelected(this)) 
        {
            gameManager.AddToSelected(new List<IClickable>() { this });
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
        if(selectionCircle != null)
        selectionCircle.enabled = false;
    }*/
}
