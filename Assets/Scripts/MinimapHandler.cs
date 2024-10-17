using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapHandler : MonoBehaviour
{
    [SerializeField] public float aboveWaterHeight;
    [SerializeField] public float underwaterHeight;
    public Transform target; //the object that we want to follow in this case: the FPScontroller

    // Update is called once per frame
    void Update()
    {
        AdjustCameraXZPlane();
    }


    //perform this after all the functions in update are done
    void LateUpdate()
    {
        RotateMiniMapCam();
    }

    //set the camera height at a particular value for under and above water only translate in XZ plane to follow the character
    private void AdjustCameraXZPlane() {
        if (Underwater.isUnderwater)
        {
            transform.position = new Vector3(target.position.x, underwaterHeight, target.position.z);
        }
        else
        {
            transform.position = new Vector3(target.position.x, aboveWaterHeight, target.position.z);
        }
    }
    private void RotateMiniMapCam() {
        int a = (int)transform.rotation.z;
        int b = (int)target.rotation.y;

        if (Math.Abs(a - b) > 1)
        {
            transform.eulerAngles = new Vector3(90f, 0f, target.rotation.y);

        }
    }
}