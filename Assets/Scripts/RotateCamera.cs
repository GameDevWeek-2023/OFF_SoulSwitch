using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float speed;

    private float _angleY = 0;

    private void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>()*speed;

        float offsetX = input.x;
        float offsetY = -input.y;

        transform.localRotation *= Quaternion.Euler(0, offsetX, 0);

        _angleY += offsetY;
        _angleY = Mathf.Clamp(_angleY, -89, 89);
        cam.localRotation = Quaternion.Euler(_angleY, 0, 0);
    }
}
