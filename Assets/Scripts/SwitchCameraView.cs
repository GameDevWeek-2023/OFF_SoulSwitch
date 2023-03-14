using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCameraView : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCam;
    [SerializeField] private GameObject thirdPersonCam;
    [SerializeField] private PlayerInput firstPersonInput;
    [SerializeField] private PlayerInput thirdPersonInput;
    
    private bool _isFirstPerson = true;

    private void Awake()
    {
        firstPersonCam.SetActive(true);
        thirdPersonCam.SetActive(false);

        firstPersonInput.enabled = true;
        thirdPersonInput.enabled = false;
    }

    private void OnSwitchCam()
    {
        if (_isFirstPerson) {
            firstPersonCam.SetActive(false);
            thirdPersonCam.SetActive(true);
            
            firstPersonInput.enabled = false;
            thirdPersonInput.enabled = true;
        }
        else
        {
            firstPersonCam.SetActive(true);
            thirdPersonCam.SetActive(false);
            
            firstPersonInput.enabled = true;
            thirdPersonInput.enabled = false;
        }

        _isFirstPerson = !_isFirstPerson;
    }
    
}
