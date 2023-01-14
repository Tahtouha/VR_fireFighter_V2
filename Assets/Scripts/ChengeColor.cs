using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChengeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(PrintWhenGrabed);
    }

    private void PrintWhenGrabed(ActivateEventArgs arg0)
    {
        print("i have been grabbed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
