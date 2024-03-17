using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class RecenterPlayer : MonoBehaviour
{
    public Transform Target;

    public void Start()
    {
        Recenter();
    }
    public void Recenter()
    {
        XROrigin xrOrigin = GetComponent<XROrigin>();
        xrOrigin.MoveCameraToWorldLocation(Target.position);
        xrOrigin.MatchOriginUpCameraForward(Target.up, Target.forward);
    }
}
