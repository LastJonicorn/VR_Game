using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerCalibration : MonoBehaviour
{

    public Transform vrHeadset; // Reference to the VR headset Transform
    public float desiredHeight = 1.6f; // Desired height for the player

    // Start is called before the first frame update
    void Start()
    {
        //Reset the position of HMD
        Vector3 newPosition = new Vector3(vrHeadset.position.x, desiredHeight, vrHeadset.position.z);
        vrHeadset.position = newPosition;
    }

}


