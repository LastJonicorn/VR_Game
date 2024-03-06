using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SliceObject : MonoBehaviour
{

    public Transform StartSlicePoint;
    public Transform EndSlicePoint;
    public LayerMask SliceableLayer;
    public VelocityEstimator VelocityEstimator;

    public Material CrossSectionMaterial;
    public float CutForce = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    public void Slice(GameObject Target)
    {
        Vector3 velocity = VelocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(EndSlicePoint.position - StartSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = Target.Slice(EndSlicePoint.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(Target, CrossSectionMaterial);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(Target, CrossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(Target);

        }
    }

    public void SetupSlicedComponent(GameObject SlicedObject)
    {
        SlicedObject.layer = LayerMask.NameToLayer("Sliceable");
        Rigidbody rb = SlicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = SlicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(CutForce, SlicedObject.transform.position, 1);
    }
}
