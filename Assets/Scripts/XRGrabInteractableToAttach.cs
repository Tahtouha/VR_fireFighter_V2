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
        if(args.interactableObject.transform.CompareTag("left Hand"))
        {
            attachTransform = LeftAttachTransform;
        }
        else if (args.interactableObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = RightAttachTransform;
        }
        base.OnSelectEntered(args);
    }

}
