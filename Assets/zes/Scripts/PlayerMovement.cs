using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float accel = 5.0f;
    public Rigidbody rb;
    Vector2 movementInput;
    public Transform camera;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        movementInput = new Vector2(x, y).normalized;
    }
    
    void FixedUpdate()
    {
        Vector3 movement = camera.parent.forward * movementInput.y + camera.parent.right * movementInput.x;
        
        Vector3 currentVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        Vector3 finalForce = (movement * maxSpeed - currentVelocity) * accel;
        
        rb.AddForce(finalForce, ForceMode.Acceleration);
    }
}
