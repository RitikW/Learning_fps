using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerlook : MonoBehaviour
{
    private float mouseSpeed = 50f;
    public Transform Playerbody;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Playerbody.Rotate(Vector3.up * mouseX);
    }
}