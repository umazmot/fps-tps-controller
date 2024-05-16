using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third Person Camera<br/>
/// Aattach this script to the camera object.
/// Then set the player character to the field "target."
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    // Player object to be followed
    public GameObject target;
    // Camera rotation sensitivity
    public float sensitivity = 1000f;
    // Camera initial position
    private Vector3 initPos;
    // Horizontal rotation angle
    private float xRot = 0f;
    // Vertical rotation angle
    private float yRot = 0f;
    // Maximum angle for vertical rotation
    private float yRotMax = 60f;
    // Minimum angle for vertical rotation
    private float yRotMin = -60f;

    void Start()
    {
        initPos = transform.position;
    }

    void LateUpdate()
    {
        xRot += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        yRot -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yRotMin, yRotMax);

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = target.transform.position + rot * initPos;
        transform.SetPositionAndRotation(pos, rot);
    }
}
