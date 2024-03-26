using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVEMENT : MonoBehaviour
{
    public float acceleration = 10F;
    public float jumpForce = 9F;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1F;
    public float maxSlopeAngle = 45F;

    public COOLDOWN coyoteTime;
    public COOLDOWN bufferJump;

    public LayerMask groundLayerMask;

    public bool IsGrounded
    {
        get { return _isGrounded; }
    }
    public bool IsJumping
    {
        get { return _isJumping; }
    }
    public bool IsRunning
    {
        get { return _isRunning; }
    }

    public bool IsFalling
    {
        get { return _isFalling; }
    }

    public bool flipAnim = false;

    protected bool _isGrounded = false;
    protected bool _isRunning = false;
    protected bool _isJumping = false;
    protected bool _isFalling = false;
    protected bool _canJump = true;

    protected Vector2 _inputDirection;
    protected Rigidbody2D _rigidbody2D;
    protected BoxCollider2D _collider2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleMovement();
        Flip();
    }

    protected virtual void HandleInput() { }

    protected virtual void HandleMovement()
    {
        Vector3 targetVelocity = Vector3.zero;

        if (_isGrounded == true && _isJumping == false)
            targetVelocity = new Vector2(_inputDirection.x * (acceleration), 0F);
        else
            targetVelocity = new Vector2(_inputDirection.x * (acceleration), _rigidbody2D.velocity.y);

        _rigidbody2D.velocity = targetVelocity;

        if (targetVelocity.x == 0)
            _isRunning = false;
        else
            _isRunning = true;
    }

    protected virtual void DoJump()
    {
        DoBufferJump();
        
        if (!_canJump)
            return;

        if (coyoteTime.CurrentProgress == COOLDOWN.Progress.Finished)
            return;

        _canJump = false;
        _isJumping = true;
        _isFalling = false;
        
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);

        coyoteTime.StopCooldown();
    }

    protected virtual void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        if (_rigidbody2D.velocity.y <= 0)
        {
            _isJumping = false;
            _isFalling = true;
        }

        if (_isGrounded && !_isJumping)
        {
            _canJump = true;
            _isFalling = false;

            if (coyoteTime.CurrentProgress != COOLDOWN.Progress.Ready)
                coyoteTime.StopCooldown();

            if (bufferJump.CurrentProgress is COOLDOWN.Progress.Started or COOLDOWN.Progress.InProgress)
                DoJump();
        }

        if (!_isGrounded && !_isJumping && coyoteTime.CurrentProgress == COOLDOWN.Progress.Ready)
            coyoteTime.StartCooldown();
    }

    protected void DoBufferJump()
    {
        bufferJump.StartCooldown();
    }

    protected virtual void Flip()
    {
        if (_inputDirection.x == 0)
            return;
        if (_inputDirection.x > 0)
            flipAnim = false;
        else if (_inputDirection.x < 0)
            flipAnim = true;
    }
}