using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] private Transform camera;
    [SerializeField] private float activateDistance;
    [SerializeField] private ActivationMethod activationMethod;

    private bool active = false;

    private void OnInteract()
    {
        Debug.Log("It works");
        RaycastHit hit;
        active = Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit,
            activateDistance);
        
        Debug.Log(hit);

        if (active && hit.transform.GetComponent<Animator>() != null)
        {
            Debug.Log("Adding Trigger");
            hit.transform.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}

public enum ActivationMethod { ActivateFirstPerson, ActivateThirdPerson}
