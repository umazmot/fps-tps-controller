using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject player;
    public Camera cam;
    public Rigidbody rb;

    //private float rotY = 0f;

    //private float rotX = 0f;

    //public Transform lookAt;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private float sensitivity = 10.0f;

    private float distance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distance = Mathf.Sqrt(cam.transform.localPosition.x * cam.transform.localPosition.x + cam.transform.localPosition.y * cam.transform.localPosition.y + cam.transform.localPosition.z * cam.transform.localPosition.z);
        Debug.Log(distance);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateCamera();
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
    }

    void RotateCamera()
    {
        //rotY = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        //rotX = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // cam.transform.RotateAround(player.transform.position, Vector3.up, rotY);
        // cam.transform.RotateAround(player.transform.position, Vector3.right, -rotX);

        //float rotYRad = rotY * Mathf.Deg2Rad;
        //float rotXRad = rotX * Mathf.Deg2Rad;

        //cam.transform.localPosition = new Vector3(distance * Mathf.Sin(rotXRad) * Mathf.Cos(rotYRad), distance * Mathf.Cos(rotXRad), distance * Mathf.Sin(rotYRad) * Mathf.Sin(rotXRad));

        //cam.transform.LookAt(transform.position);


        //float x = 90f * Mathf.Deg2Rad; //平面
        //float y = 0f * Mathf.Deg2Rad; //垂直
        // cam.transform.localPosition = new Vector3(
        //     distance * Mathf.Sin(rotYRad) * Mathf.Cos(rotXRad),
        //     distance * Mathf.Cos(rotYRad),
        //     distance * Mathf.Sin(rotYRad) * Mathf.Sin(rotXRad));
        // cam.transform.LookAt(transform.position);
        // Debug.Log("x=" + cam.transform.localPosition.x);
        // Debug.Log("y=" + cam.transform.localPosition.y);
        // Debug.Log("z=" + cam.transform.localPosition.z);

        currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
 
        currentY = Mathf.Clamp(currentY, -80.0f, 80.0f);
 
        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        cam.transform.position = transform.position + rotation * Direction;
 
        cam.transform.LookAt(transform.position);
    }
}
