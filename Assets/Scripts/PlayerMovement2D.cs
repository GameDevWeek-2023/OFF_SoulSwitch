using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
[Header("Movement")]
    [SerializeField] private float speed = 7;
    [SerializeField] private float airMultiplier = 0.5f;
    private Vector2 direction;
    private Rigidbody _rigidbody;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float jumpCooldown = 0.25f;
    private float jumpCooldownLeft = 0f;

    [Header("Ground Check")] [SerializeField]
    private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    private bool grounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        SpeedControl();
        CountJumpCooldown();
        
        // Handle drag
        if (grounded)
        {
            _rigidbody.drag = groundDrag;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }

    private void OnMove(InputValue value)
    {
        if (grounded)
        {
            direction = value.Get<Vector2>();
        }
    }
    
    private void OnJump()
    {
        if (grounded && JumpCooldownReady())
        {
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpCooldownLeft = jumpCooldown;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        var relativeDir = Vector3.forward * direction.x;

        // on ground
        if(grounded)
            _rigidbody.AddForce(relativeDir.normalized * (speed * 10f), ForceMode.Force);

        // in air
        else if(!grounded)
            _rigidbody.AddForce(relativeDir.normalized * (speed * 10f * airMultiplier), ForceMode.Force);
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void CountJumpCooldown()
    {
        if (jumpCooldownLeft > 0f)
        {
            jumpCooldownLeft = Mathf.Clamp(jumpCooldownLeft - Time.fixedDeltaTime, 0, jumpCooldown);
        }
    }

    private bool JumpCooldownReady()
    {
        return (jumpCooldownLeft == 0);
    }
}
