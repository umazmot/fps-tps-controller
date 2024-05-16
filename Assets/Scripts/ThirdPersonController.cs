using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third Person Controller<br/>
/// Aattach this script to the player character object.
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    // Moving Speed
    public float speed = 5f;
    // Sprint speed
    public float sprintSpeed = 10f;
    // Rigidbody
    private Rigidbody rb;
    // Fly mode
    private bool isFlying = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.useGravity = !rb.useGravity;
            isFlying = !isFlying;
        }

        float xIn = Input.GetAxis("Horizontal");
        float zIn = Input.GetAxis("Vertical");

        // Idle state
        if (xIn == 0f && zIn == 0f)
        {
            if (isFlying)
            {
                // Reset the non-zero Y velocity due to colliding with something in fly mode.
                rb.velocity = Vector3.zero;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), 0.1f);
            return;
        }

        Vector3 dir = Camera.main.transform.forward * zIn + Camera.main.transform.right * xIn;
        if (!isFlying)
        {
            dir.y = 0f;
        }
        Vector3 velocity = (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed) * Time.deltaTime * dir.normalized;

        transform.forward = Vector3.Slerp(transform.forward, velocity, 0.1f);
        transform.position += velocity;
    }
}
