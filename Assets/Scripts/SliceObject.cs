using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using System;

public class SliceObject : MonoBehaviour
{

    public Transform StartSlicePoint;
    public Transform EndSlicePoint;
    public LayerMask SliceableLayer;
    public VelocityEstimator VelocityEstimator;
    public Material CrossSectionMaterial;
    public float CutForce = 500;

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(StartSlicePoint.position, EndSlicePoint.position, out RaycastHit hit, SliceableLayer);
        if(hasHit)
        {
            GameObject Target = hit.transform.gameObject;
            Slice(Target);
        }
    }

    //Logic for slicing objects in two
    public void Slice(GameObject Target)
    {
        Vector3 velocity = VelocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(EndSlicePoint.position - StartSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = Target.Slice(EndSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(Target, CrossSectionMaterial);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(Target, CrossSectionMaterial);

            // Check if the lower hull's tag is "Test" before removing Rigidbody component
            if (Target.CompareTag("Tatami"))
            {
                // Perform actions if the tag matches
                lowerHull.layer = LayerMask.NameToLayer("Sliceable"); // Set the layer to "Sliceable"
                Rigidbody rb = lowerHull.AddComponent<Rigidbody>(); //Added 11.3
                rb.isKinematic = true; //Added 11.3
                lowerHull.tag = "Tatami";
                MeshCollider collider = lowerHull.AddComponent<MeshCollider>();
                collider.convex = true;
            }
            else
            {
                // Perform actions if the tag does not match
                SetupSlicedComponent(lowerHull);
            }

            Destroy(Target);
        }
    }

    //Logic for the two new gameObjects created by the Slice
    public void SetupSlicedComponent(GameObject SlicedObject)
    {
        SlicedObject.layer = LayerMask.NameToLayer("Sliceable"); // Set the layer to "Sliceable"
        Rigidbody rb = SlicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = SlicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(CutForce, SlicedObject.transform.position, 1);
    }
}
