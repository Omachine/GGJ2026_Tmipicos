using System;
using UnityEngine;

public class FPCameraController : MonoBehaviour
{
    public static FPCameraController instance;
    
    public float mouseSensitivity = 100.0f;
    public Transform fpCamera;
    private float xRotation, yRotation;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpCamera = transform.GetChild(0);
    }

    // Update is called once per frame
    public void UpdateCamera()
    {
        float mousex = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation += mousex;
        
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        fpCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
