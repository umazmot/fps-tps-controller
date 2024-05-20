using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// First/Third Person Camera<br/>
/// Aattach this script to the camera object.
/// Then set the player character to the field "target."
/// </summary>
public class XPersonCamera : MonoBehaviour
{
    // Player object to be followed
    public GameObject target;
    // TPS mode or FPS mode
    private bool isTPS = true;
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
    // Offset for the center of the target in TPS mode
    private Vector3 offsetTPS = Vector3.up;
    // Offset for the center of the target in FPS mode
    private Vector3 offsetFPS = Vector3.up * 1.5f;

    void Start()
    {
        initPos = transform.position - offsetTPS;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isTPS = !isTPS;
            target.transform.localScale = isTPS ? Vector3.one : Vector3.zero;
            distance = isTPS ? 1f : 0f;
        }

        float xIn = Input.GetAxis("Mouse X") * rotSensitivity * Time.deltaTime;
        float yIn = Input.GetAxis("Mouse Y") * rotSensitivity * Time.deltaTime;
        float scrollIn = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;

        Vector3 targetPos = target.transform.position + (isTPS ? offsetTPS : offsetFPS);
        Quaternion shiftRot = Quaternion.Euler(Mathf.Clamp(yRot - yIn, yRotMin, yRotMax), xRot + xIn, 0f);
        float shiftDistance = Mathf.Clamp(distance + scrollIn, minDistance, maxDistance);
        Vector3 shiftPos = targetPos + shiftRot * (shiftDistance * initPos);

        // Auto zoom in
        if (isTPS && IsBlocked(shiftPos, targetPos, out RaycastHit hit))
        {
            distance -= hit.distance / shiftPos.magnitude;
        }
        else
        {
            xRot += xIn;
            yRot = Mathf.Clamp(yRot - yIn, yRotMin, yRotMax);
        }
        distance = isTPS ? Mathf.Clamp(distance + scrollIn, minDistance, maxDistance) : 0f;

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = targetPos + rot * (distance * initPos);
        transform.SetPositionAndRotation(pos, rot);
    }

    // Returns true if something is between the camera and the player.
    private bool IsBlocked(Vector3 camera, Vector3 player, out RaycastHit hit)
    {
        Vector3 dir = player - camera;
        return Physics.Raycast(camera, dir, out hit, dir.magnitude) && !hit.collider.transform.root.CompareTag("Player");
    }
}
