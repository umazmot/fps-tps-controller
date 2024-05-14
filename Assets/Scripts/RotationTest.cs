using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    // その場での回転量、Y軸
    float angleX = 0.0f;
    // その場での回転量、Y軸
    float angleY = 0.0f;
    // その場での回転量、Z軸
    float angleZ = 0.0f; 
    // 球の半径
    float distance = 4.0f;

    float xRotation = 0.0f;

    float yRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        distance = (float) Math.Sqrt(
            transform.position.x * transform.position.x
            + transform.position.y * transform.position.y
            + transform.position.z * transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //SelfRotation();
        //Revolition();
        SetAngle();
        MoveSphereSurface();
    }

    void SetAngle() {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(x) < 0.001f && Mathf.Abs(y) < 0.001f) {
            return;
        }
        // 水平角度
        xRotation -= x * 1000.0f * Time.deltaTime;
        // 垂直角度
        yRotation -= y * 1000.0f * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, 0.0f, 60.0f);
    }

    // 球面上を移動
    void MoveSphereSurface() {
        // 回転角
        float xRot = xRotation * Mathf.Deg2Rad;
        float yLot = yRotation * Mathf.Deg2Rad;
        // 座標
        float x = distance * Mathf.Cos(yLot) * Mathf.Cos(xRot);
        float y = distance * Mathf.Sin(yLot);
        float z = distance * Mathf.Cos(yLot) * Mathf.Sin(xRot);
        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(z);
        //transform.position = new Vector3(x, y, z);
        transform.localPosition = new Vector3(x, y, z);
        transform.LookAt(Vector3.zero);
    }

    // 公転
    void Revolition()
    {
        // 原点中心
        Vector3 center = new Vector3(0, 0, 0);
        // Y軸が回転軸
        transform.RotateAround(center, Vector3.up, Time.deltaTime * 90);
        
    }

    // 自転
    void SelfRotation()
    {
        // X方向を軸にその場で回転(正方向から見て時計回り)
        angleX += Time.deltaTime * 90.0f;
        //transform.rotation = Quaternion.Euler(angleX, 0, 0);
        // Y方向を軸にその場で回転(正方向から見て時計回り)
        angleY += Time.deltaTime * 90.0f;
        //transform.rotation = Quaternion.Euler(0, angleY, 0);
        // Z方向を軸にその場で回転(正方向から見て時計回り)
        angleZ += Time.deltaTime * 90.0f;
        //transform.rotation = Quaternion.Euler(0, 0, angleZ);
        transform.rotation = Quaternion.Euler(angleX, angleY, angleZ);
    }
}
