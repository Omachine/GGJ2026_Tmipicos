using UnityEngine;

public class FPCameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform camera;
    private float xRotation, yRotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation += mousex;
        
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        camera.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
