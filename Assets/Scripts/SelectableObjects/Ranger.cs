using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Character
{
    [SerializeField] float arrowSpeed = 30;
    [SerializeField] Projectile projectilePrefab;

    public override void Update()
    {
        base.Update();
        RangedAttack(projectilePrefab, arrowSpeed);
    }
}
