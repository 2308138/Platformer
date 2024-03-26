using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIMATIONHANDLER : MonoBehaviour
{
    private Animator _animator;
    private MOVEMENT _movement;

    private Vector3 _initialScale = Vector3.one;
    private Vector3 _flipScale = Vector3.one;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = transform.parent.GetComponent<MOVEMENT>();

        _initialScale = transform.localScale;
        _flipScale = new Vector3(-_initialScale.x, _initialScale.y, _initialScale.z);
    }

    void Update()
    {
        HandleFlip();
        UpdateAnimator();
    }

    void HandleFlip()
    {
        if (_movement == null)
            return;

        transform.localScale = _movement.flipAnim ? _flipScale : _initialScale;
    }

    void UpdateAnimator()
    {
        if (_movement == null || _animator == null)
            return;

        _animator.SetBool("isRunning", _movement.IsRunning);
        _animator.SetBool("isJumping", _movement.IsJumping);
        _animator.SetBool("isFalling", _movement.IsFalling);
    }
}