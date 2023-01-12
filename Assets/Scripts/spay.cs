using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class spay : MonoBehaviour
{
    public ParticleSystem particleSystem;
    //public GameObject extinguish;
    public bool IsGrabbing;


    public InputActionProperty pinchAnimationAction;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem.Stop();

    }


    // Update is called once per frame
    void Update()
    {

        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        Debug.Log(triggerValue);

        if (triggerValue == 0)
        {
            particleSystem.Stop();
        }
        else
        {
            particleSystem.Play();
        }
    }





}

