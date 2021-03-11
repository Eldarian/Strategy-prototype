using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class Enemy : Character
{
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        FindTarget();
        base.Update();
        MeleeAttack();
    }

    private void FindTarget()
    {
        Character[] characters = FindObjectsOfType<Character>();
        Transform closest = null;
        float distanceToClosest = Mathf.Infinity;
        foreach (Character character in characters)
        {
            if (!typeof(Enemy).IsInstanceOfType(character))
            {
                if (closest == null || Vector3.Distance(transform.position, character.transform.position) < distanceToClosest)
                {
                    closest = character.transform;
                    distanceToClosest = Vector3.Distance(transform.position, closest.position);
                }
            }
        }
        if (closest == null)
        {
            closest = FindObjectOfType<Fortress>().transform;
        }
        SetObjective(closest.transform);

    }
}
