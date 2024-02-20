using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVEMENT : MonoBehaviour
{
    public float acceleration = 10F;
    public float jumpForce = 7F;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1F;
    public float maxSlopeAngle = 45F;

    public LayerMask groundLayerMask;

    private bool _isGrounded = false;
    private bool _isJumping = false;

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
    }

    void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump") == true)
            _isJumping = true;
        else
            _isJumping = false;
    }

    void HandleMovement()
    {
        Vector3 targetVelocity = Vector3.zero;

        if (_isGrounded == true && _isJumping == false)
            targetVelocity = new Vector2(_inputDirection.x * (acceleration), 0F);
        else
            targetVelocity = new Vector2(_inputDirection.x * (acceleration), _rigidbody2D.velocity.y);

        _rigidbody2D.velocity = targetVelocity;
    }

    void DoJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
    }

    void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        if (_isGrounded == true)
            DoJump();
    }
}