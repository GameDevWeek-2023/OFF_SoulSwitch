using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraView : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCam;
    [SerializeField] private GameObject thirdPersonCam;
    
    private bool _isFirstPerson = true;

    private void Awake()
    {
        firstPersonCam.SetActive(true);
        thirdPersonCam.SetActive(false);
    }

    private void OnSwitchCam()
    {
        if (_isFirstPerson) {
            firstPersonCam.SetActive(false);
            thirdPersonCam.SetActive(true);
        }
        else
        {
            firstPersonCam.SetActive(true);
            thirdPersonCam.SetActive(false);
        }

        _isFirstPerson = !_isFirstPerson;
    }
    
}
