using System.Net;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 mOffset;
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

    /*private void OnCollisionEnter(Collision collision)
    {
        isTrigger = true;
        Debug.Log(isTrigger);
    }

    private void OnCollisionExit(Collision collision)
    {
        isTrigger = false;
        Debug.Log(isTrigger);
    }*/

    /*private Rigidbody rb;
    private Vector3 mOffset;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        mOffset = transform.position - GetMouseAsWorldPoint();
    }

    void OnMouseDrag()
    {
        Vector3 target = GetMouseAsWorldPoint() + mOffset;

        // Keep Z fixed if needed
        Vector3 newPos = new Vector3(target.x, target.y, rb.position.z);

        // Move with physics, respecting collisions
        rb.MovePosition(newPos);
    }

    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouse);
    }*/

}
