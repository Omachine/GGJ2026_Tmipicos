using System.Net;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    /*private Vector3 mOffset;
    private float mZCoord;
    //private bool isTrigger = false;
    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    bool WouldCollide(Vector3 delta)
    {
        Vector3 center = col.bounds.center + delta;
        Vector3 halfExtents = col.bounds.extents;
        Quaternion rotation = transform.rotation;

        Collider[] hits = Physics.OverlapBox(
            center,
            halfExtents,
            rotation
        );

        foreach (Collider hit in hits)
        {
            if (hit != col)
                return true;
        }

        return false;
    }

    void OnMouseDown()
    {

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()

    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        //transform.position.x = GetMouseAsWorldPoint().x + mOffset.x;

        Vector3 target = GetMouseAsWorldPoint() + mOffset;
        Vector3 current = transform.position;

        float dx = target.x - current.x;
        float dy = target.y - current.y;

        // ----- X AXIS -----
        if (dx != 0f)
        {
            Vector3 deltaX = new Vector3(dx, 0f, 0f);
            if (!WouldCollide(deltaX))
            {
                transform.position += deltaX;
            }
        }

        // ----- Y AXIS -----
        if (dy != 0f)
        {
            Vector3 deltaY = new Vector3(0f, dy, 0f);
            if (!WouldCollide(deltaY))
            {
                transform.position += deltaY;
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }*/

    /*private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody rb;
    [SerializeField] float moveSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 1f;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        rb.isKinematic = false;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = true;
    }

    void OnMouseDrag()
    {
        //transform.position = GetMouseAsWorldPoint() + mOffset;
        rb.MovePosition(transform.position + mOffset);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }*/

    Rigidbody rb;
    Vector3 mOffset;
    float mZCoord;
    bool dragging;

    [Header("Tuning")]
    public float forceStrength = 1f;
    public float maxSpeed = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnMouseDown()
    {
        dragging = true;

        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseAsWorldPoint();
        rb.isKinematic = false;
    }

    void OnMouseUp()
    {
        dragging = false;
        rb.linearVelocity = Vector3.zero; // stop immediately when released
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (!dragging)
            return;

        Vector3 target = GetMouseAsWorldPoint() + mOffset;
        Vector3 direction = target - rb.position;

        // Apply force toward mouse
        rb.AddForce(direction * forceStrength, ForceMode.Acceleration);

        // Clamp max speed (VERY important)
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mouse);
    }
}
