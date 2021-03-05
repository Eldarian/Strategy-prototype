﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    GameObject fortress;

    public override void Start()
    {
        base.Start();
        print("Child Start");
        fortress = GameObject.FindGameObjectWithTag("Finish");
        stopDistance = 7.5f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        MoveToPoint(fortress.transform.position, stopDistance);
    }
}