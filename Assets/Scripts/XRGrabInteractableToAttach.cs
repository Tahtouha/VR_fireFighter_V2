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

    private void Start()
    {
       // child = gameObject.transform.Find("Sphere").gameObject;
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        //gameObject.GetComponent<SphereCollider>().enabled = true;

        attachTransform = RightAttachTransform;
        //child.GetComponent<SphereCollider>().enabled = true;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {

        //gameObject.GetComponent<SphereCollider>().enabled = false;
        //child.GetComponent<SphereCollider>().enabled = false;
        base.OnSelectExited(args);
    }

}
