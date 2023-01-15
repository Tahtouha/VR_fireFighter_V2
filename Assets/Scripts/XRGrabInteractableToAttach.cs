using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableToAttach : XRGrabInteractable
{
    public Transform RightAttachTransform;
    public Transform LeftAttachTransform;

    

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        gameObject.GetComponent<SphereCollider>().enabled = true;

        
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {

        gameObject.GetComponent<SphereCollider>().enabled = false;
        base.OnSelectExited(args);
    }

}
