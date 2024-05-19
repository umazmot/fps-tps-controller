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
    // Camera rotation sensitivity
    public float rotSensitivity = 1000f;
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
    private float distance = 1f;
    private float maxDistance = 2f;
    private float minDistance = 0.5f;
    private float zoomSensitivity = 100f;

    void Start()
    {
        initPos = transform.position;
    }

    void LateUpdate()
    {
        distance += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        xRot += Input.GetAxis("Mouse X") * rotSensitivity * Time.deltaTime;
        yRot -= Input.GetAxis("Mouse Y") * rotSensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yRotMin, yRotMax);

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = target.transform.position + rot * (distance * initPos);
        transform.SetPositionAndRotation(pos, rot);
    }
}
