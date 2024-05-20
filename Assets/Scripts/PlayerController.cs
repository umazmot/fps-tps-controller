using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Controller<br/>
/// Aattach this script to the player character object.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Moving speed
    public float speed = 5f;
    // Sprint speed
    public float sprintSpeed = 10f;
    // Jump force
    public float jumpForce = 8f;
    // Rigidbody
    private Rigidbody rb;
    // Fly mode
    private bool isFlying = false;
    // Ray range
    private float rayRange = 1.1f;

    void Start()
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

        if (Input.GetKeyDown(KeyCode.Space) && !isFlying && Physics.Raycast(transform.position + Vector3.up, Vector3.down, rayRange))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        float xIn = Input.GetAxis("Horizontal");
        float zIn = Input.GetAxis("Vertical");

        // Idle state
        if (xIn == 0f && zIn == 0f)
        {
            // Reset the non-zero Y velocity due to colliding with something in fly mode.
            if (isFlying)
            {
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
