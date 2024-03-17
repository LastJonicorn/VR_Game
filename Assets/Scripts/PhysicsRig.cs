using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{

    public Transform LeftController;
    public Transform RightController;

    public ConfigurableJoint LeftHandJoint;
    public ConfigurableJoint RightHandJoint;

    void FixedUpdate()
    {
        LeftHandJoint.targetPosition = LeftController.localPosition;
        LeftHandJoint.targetRotation = LeftController.localRotation;

        RightHandJoint.targetPosition = RightController.localPosition;
        RightHandJoint.targetRotation = RightController.localRotation;

    }
}
