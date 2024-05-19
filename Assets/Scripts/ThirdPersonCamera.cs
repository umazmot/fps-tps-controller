using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    // Initial position
    private Vector3 initPos;
    // Zoom scale i.e. distance from the target
    private float zoomScale = 1f;
    // Maximum zoom scale
    private float maxZoomScale = 2f;
    // Minimum zoom scale
    private float minZoomScale = 0.5f;
    // Zoom sensitivity
    private float zoomSensitivity = 100f;
    // Horizontal rotation angle
    private float xRot = 0f;
    // Vertical rotation angle
    private float yRot = 0f;
    // Maximum angle for vertical rotation
    private float yRotMax = 60f;
    // Minimum angle for vertical rotation
    private float yRotMin = -60f;
    // Rotation sensitivity
    private float rotSensitivity = 1000f;

    void Start()
    {
        initPos = transform.position;
    }

    void LateUpdate()
    {
        zoomScale += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        zoomScale = Mathf.Clamp(zoomScale, minZoomScale, maxZoomScale);

        xRot += Input.GetAxis("Mouse X") * rotSensitivity * Time.deltaTime;
        yRot -= Input.GetAxis("Mouse Y") * rotSensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yRotMin, yRotMax);

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = target.transform.position + rot * (zoomScale * initPos);
        transform.SetPositionAndRotation(pos, rot);
    }
}
