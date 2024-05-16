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

    void LateUpdate()
    {
        float xIn = Input.GetAxis("Horizontal");
        float zIn = Input.GetAxis("Vertical");
        if (xIn == 0f && zIn == 0f)
        {
            return;
        }

        Vector3 dir = Camera.main.transform.forward * zIn + Camera.main.transform.right * xIn;
        dir.y = 0f;
        Vector3 velocity = speed * Time.deltaTime * dir.normalized;

        transform.forward = Vector3.Slerp(transform.forward, velocity, 0.1f);
        transform.position += velocity;
    }
}
