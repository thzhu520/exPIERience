using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Controls a cameras position and rotation based on playerSpeed and mouse input
    // Currently not used

    public float camSpeedH = 2.0f;
    public float camSpeedV = 2.0f;

    public float playerMoveSpeed = 3.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        yaw += camSpeedH * Input.GetAxis("Mouse X");
        pitch -= camSpeedV * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -60f, 90f); // the vertical rotation range

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetMouseButton(0))
        {
            transform.position += Camera.main.transform.forward * playerMoveSpeed * Time.deltaTime;
        }
    }
}

