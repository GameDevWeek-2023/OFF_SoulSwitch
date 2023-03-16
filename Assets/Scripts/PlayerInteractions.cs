using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteractions : MonoBehaviour
{

    [FormerlySerializedAs("camera")] [SerializeField] private Transform cam;
    [SerializeField] private float activateDistance;
    [SerializeField] private ActivationMethod activationMethod;

    private bool _active = false;

    private void OnInteract()
    {
        Debug.Log("It works");
        RaycastHit hit;
        _active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit,
            activateDistance);
        
        Debug.Log(hit);

        if (_active && hit.transform.GetComponent<Animator>() != null)
        {
            Debug.Log("Adding Trigger");
            hit.transform.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}

public enum ActivationMethod { ActivateFirstPerson, ActivateThirdPerson}
