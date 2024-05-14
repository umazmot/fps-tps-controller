using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third Person Camera<br/>
/// Make the camera object a child of the player character.
/// Also attach this script to the camera object.
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    // Horizontal rotation angle
    private float xRot = 0f;
    // Vertical rotation angle
    private float yRot = 0f;
    // Maximum angle for vertical rotation
    private float yRotMax = 60f;
    // Minimum angle for vertical rotation
    private float yRotMin = -60f;
    // Camera initial local position
    private Vector3 initPos = Vector3.zero;
    // Camera rotation sensitivity
    public float sensitivity = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    void LateUpdate()
    {
        float xIn = Input.GetAxis("Mouse X");
        float yIn = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(xIn) < 0.001f && Mathf.Abs(yIn) < 0.001f)
        {
            return;
        }

        xRot += xIn * sensitivity * Time.deltaTime;
        yRot -= yIn * sensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yRotMin, yRotMax);

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = rot * initPos;
        transform.SetLocalPositionAndRotation(pos, rot);
    }
}
