﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : Character
{
    public override void Update()
    {
        base.Update();
        MeleeAttack();
    }
}
