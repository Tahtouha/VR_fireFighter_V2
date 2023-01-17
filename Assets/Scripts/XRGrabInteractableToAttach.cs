using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableToAttach : XRGrabInteractable
{
    public Transform RightAttachTransform;
    public Transform LeftAttachTransform;
    GameObject child;
    private GameObject magicHint;

    private void Start()
    {
        magicHint = GameObject.Find("magicHint");
       // child = gameObject.transform.Find("Sphere").gameObject;
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        magicHint.SetActive(false);
        int layerIndex = LayerMask.NameToLayer("Spray");
        gameObject.layer = layerIndex;
        if (gameObject.name == "Fire Extinguisher")
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            attachTransform = RightAttachTransform;
        }
        else
        {
            attachTransform = RightAttachTransform;

        }


        //child.GetComponent<SphereCollider>().enabled = true;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        int layerIndex = LayerMask.NameToLayer("Interactable");
        gameObject.layer = layerIndex;

        if (gameObject.name == "Fire Extinguisher")
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            attachTransform = RightAttachTransform;
        }

        else
        {
            attachTransform = RightAttachTransform;
        }
        //child.GetComponent<SphereCollider>().enabled = false;
        base.OnSelectExited(args);
    }


}
