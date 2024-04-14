using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowPullInteraction : XRBaseInteractable
{

    public static event Action<float> PullActionReleased;

    public Transform Start, End;
    public GameObject Notch;

    public float PullAmount { get; private set; } = 0.0f;

    private LineRenderer _lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
    }

    public void Release()
    {
        PullActionReleased?.Invoke(PullAmount);
        pullingInteractor = null;
        PullAmount = 0f;
        Notch.transform.localPosition = new Vector3(Notch.transform.localPosition.x, Notch.transform.localPosition.y, 0f);
        UpdateString();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                PullAmount = CalculatePull(pullPosition);

                UpdateString();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - Start.position;
        Vector3 targetDirection = End.position - Start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    private void UpdateString()
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(Start.transform.localPosition.z, End.transform.localPosition.z, PullAmount);
        Notch.transform.localPosition = new Vector3(Notch.transform.localPosition.x, Notch.transform.localPosition.y, linePosition.z +.2f);
        _lineRenderer.SetPosition(1, linePosition);
    }
}
