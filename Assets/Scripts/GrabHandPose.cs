using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandData RightHandPose;
    private Vector3 StartHandPosition;
    private Vector3 FinalHandPosition;
    private Quaternion StartHandRotataion;
    private Quaternion FinalHandRotation;

    private Quaternion[] StartFingerRotation;
    private Quaternion[] FinalFingerRotation;


    // I don't fucking know
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnSetPose);

        RightHandPose.gameObject.SetActive(false);
    }

    //Set custom hand position
    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if(arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.Animator.enabled = false;

            SetHandDataValues(handData, RightHandPose);
            SetHandData(handData, FinalHandPosition, FinalHandRotation, FinalFingerRotation);
        }
    }

    //Set hand position back to default after letting go of object
    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.Animator.enabled = true;

            SetHandData(handData, StartHandPosition, StartHandRotataion, StartFingerRotation);
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        StartHandPosition = new Vector3
            (
                h1.Root.localPosition.x / h1.Root.localScale.x, 
                h1.Root.localPosition.y / h1.Root.localScale.y, 
                h1.Root.localPosition.z / h1.Root.localScale.z
            );
        FinalHandPosition = new Vector3
            (
                h2.Root.localPosition.x / h2.Root.localScale.x, 
                h2.Root.localPosition.y / h2.Root.localScale.y, 
                h2.Root.localPosition.z / h2.Root.localScale.z
            );

        StartHandRotataion = h1.Root.localRotation;
        FinalHandRotation = h2.Root.localRotation;

        StartFingerRotation = new Quaternion[h1.FingerBones.Length];
        FinalFingerRotation = new Quaternion[h1.FingerBones.Length];

        for(int i = 0; i < h1.FingerBones.Length; i++)
        {
            StartFingerRotation[i] = h1.FingerBones[i].localRotation;
            FinalFingerRotation[i] = h2.FingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBoneRotation)
    {
        h.Root.localPosition = newPosition;
        h.Root.localRotation = newRotation;

        for(int i = 0; i < newBoneRotation.Length; i++)
        {
            h.FingerBones[i].localRotation = newBoneRotation[i];
        }
    }
}
