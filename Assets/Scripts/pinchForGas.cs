using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class pinchForGas : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
        XRGrabInteractable xRGrab = GetComponent<XRGrabInteractable>();
        xRGrab.activated.AddListener(FireGas);
    }

    private void FireGas(ActivateEventArgs arg0)
    {
        Debug.Log("Grabbed");
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
