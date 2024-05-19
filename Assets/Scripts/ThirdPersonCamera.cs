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
    // Distance from the target i.e. zoom scale
    private float distance = 1f;
    // Maximum distance from the target
    private float maxDistance = 2f;
    // Minimum distance from the target
    private float minDistance = 0.5f;
    // Zoom sensitivity
    private float zoomSensitivity = 100f;
    // Horizontal rotation angle
    private float xRot = 0f;
    // Vertical rotation angle
    private float yRot = 0f;
    // Maximum angle of vertical rotation
    private float yRotMax = 60f;
    // Minimum angle of vertical rotation
    private float yRotMin = -60f;
    // Rotation sensitivity
    private float rotSensitivity = 1000f;

    void Start()
    {
        initPos = transform.position;
    }

    void LateUpdate()
    {
        distance += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        Vector3 rayDir = target.transform.position - transform.position;
        Debug.DrawRay(transform.position, rayDir, Color.red, rayDir.magnitude);
        if (Physics.Raycast(transform.position, rayDir, out RaycastHit hit, rayDir.magnitude))
        {
            if (!hit.collider.transform.root.CompareTag("Player"))
            {
                distance -= hit.distance / transform.position.magnitude;
            }
        }
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        Debug.Log(distance);

        xRot += Input.GetAxis("Mouse X") * rotSensitivity * Time.deltaTime;
        yRot -= Input.GetAxis("Mouse Y") * rotSensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yRotMin, yRotMax);

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = target.transform.position + rot * (distance * initPos);
        transform.SetPositionAndRotation(pos, rot);
    }
}
