using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{

    public GameObject rightTeleportation;

    public InputActionProperty rightActivate;

    public InputActionProperty rightCancel;

    public XRRayInteractor RightRay;

    // Update is called once per frame
    void Update()
    {
        //Check if right ray is hovering and if true don't show teleportation ray
        bool IsRightRayHovering = RightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);

        rightTeleportation.SetActive(!IsRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1);
    }
}
