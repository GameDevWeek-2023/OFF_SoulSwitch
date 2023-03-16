using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCameraView : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Transform firstPersonCamHolder;
    [SerializeField] private GameObject firstPersonCam;
    [SerializeField] private Transform thirdPersonCamHolder;
    [SerializeField] private GameObject thirdPersonCam;
    
    [Header("Inputs")]
    [SerializeField] private PlayerInput firstPersonInput;
    [SerializeField] private PlayerInput thirdPersonInput;
    
    private bool _isFirstPerson = true;

    // Camera Move Animation
    [SerializeField] private float animationSpeed = 1.0f;
    private bool isCameraMoving = false;
    private GameObject movingCam;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 endPosition;
    private Quaternion endRotation;

    private float startTime;
    private float journeyLength;

    private PlayerInput _playerInput;

    private void Awake()
    {
        firstPersonCam.SetActive(true);
        thirdPersonCam.SetActive(false);
        firstPersonInput.enabled = true;
        thirdPersonInput.enabled = false;
    }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnSwitchCam()
    {
        _playerInput.enabled = false;
        thirdPersonInput.enabled = false;
        firstPersonInput.enabled = false;

        if (_isFirstPerson)
        {
            movingCam = firstPersonCam;
            
            startPosition = firstPersonCamHolder.position;
            endPosition = thirdPersonCamHolder.position;

            startRotation = firstPersonCamHolder.rotation;
            endRotation = thirdPersonCamHolder.rotation;
        }
        else
        {
            movingCam = thirdPersonCam;
            
            startPosition = thirdPersonCamHolder.position;
            endPosition = firstPersonCamHolder.position;

            startRotation = thirdPersonCamHolder.rotation;
            endRotation = firstPersonCamHolder.rotation;
        }

        journeyLength = Vector3.Distance(startPosition, endPosition);
        startTime = Time.time;
        isCameraMoving = true;
    }

    private void Update()
    {
        if (isCameraMoving)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * animationSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            
            // Set our position as a fraction of the distance between the markers.
            movingCam.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            movingCam.transform.rotation = Quaternion.Lerp(startRotation, endRotation, fractionOfJourney);

            // Determine end of animation
            fractionOfJourney = Mathf.Clamp(fractionOfJourney, 0, 1);
            if (fractionOfJourney == 1f)
            {
                finishTransition();
            }
        }
    }

    private void finishTransition()
    {
        isCameraMoving = false;
        Transform returnTransform;
        
        
        if (_isFirstPerson) {
            firstPersonCam.SetActive(false);
            thirdPersonCam.SetActive(true);

            returnTransform = firstPersonCamHolder;
            
            thirdPersonInput.enabled = true;
        }
        else
        {
            firstPersonCam.SetActive(true);
            thirdPersonCam.SetActive(false);

            returnTransform = thirdPersonCamHolder;
            
            firstPersonInput.enabled = true;
        }
        
        movingCam.transform.position = returnTransform.position;
        movingCam.transform.rotation = returnTransform.rotation;

        _isFirstPerson = !_isFirstPerson;
        _playerInput.enabled = true;
    }
}
