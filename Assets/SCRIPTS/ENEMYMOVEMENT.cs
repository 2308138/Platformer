using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMYMOVEMENT : MOVEMENT
{
    public Transform target;

    protected override void HandleInput()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        if (target == null)
            return;

        Vector2 targetDirection = target.position - transform.position;
        targetDirection = targetDirection.normalized;

        _inputDirection = targetDirection;
    }
}