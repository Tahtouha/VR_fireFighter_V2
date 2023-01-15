using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class FireCollisin : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        Debug.Log(triggerValue);
        if (collision.gameObject.name == "Fire Extinguisher" && triggerValue != 0)
        {
            Debug.Log("Fire GONE!!!");
            Destroy(gameObject);
        }
    }
}
