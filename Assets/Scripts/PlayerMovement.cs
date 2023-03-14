using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sprintModifier = 1.5f;
    
    private Vector2 direction;
    private bool sprint;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnSprint(InputValue value)
    {
        sprint = (value.Get<float>() == 1);
    }

    private void FixedUpdate()
    {
        float velocity = sprint ? speed * sprintModifier : speed;

        var right = direction.x * transform.right;
        var forward = direction.y * transform.forward;
        var relativeDir = right + forward;
        
        //transform.position += relativeDir * (velocity * Time.fixedDeltaTime);
        
        _rigidbody.AddForce(relativeDir * velocity, ForceMode.Acceleration);
    }
}
