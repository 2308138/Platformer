using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOOMBA : MOVEMENT

{
    protected bool _flipDirection = false;

    protected override void HandleInput()
    {
        if (_flipDirection)
            _inputDirection = Vector2.left;
        else
            _inputDirection = Vector2.right;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("BOUNDARY"))
                return;

        _flipDirection = !_flipDirection;
    }
}