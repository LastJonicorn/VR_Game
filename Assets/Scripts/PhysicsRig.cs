using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{

    public Transform PlayerHead;
    public Transform LeftController;
    public Transform RightController;

    public CapsuleCollider BodyCollider;

    public float BodyHeightMin = 0.5f;
    public float BodyHeightMax = 2;

    //public ConfigurableJoint LeftHandJoint;
    //public ConfigurableJoint RightHandJoint;

    void FixedUpdate()
    {
        BodyCollider.height = Mathf.Clamp(PlayerHead.localPosition.y, BodyHeightMin, BodyHeightMax);
        BodyCollider.center = new Vector3(PlayerHead.localPosition.x, BodyCollider.height / 2, PlayerHead.localPosition.z);

        //LeftHandJoint.targetPosition = LeftController.localPosition;
        //LeftHandJoint.targetRotation = LeftController.localRotation;

        //RightHandJoint.targetPosition = RightController.localPosition;
        //RightHandJoint.targetRotation = RightController.localRotation;

    }
}
