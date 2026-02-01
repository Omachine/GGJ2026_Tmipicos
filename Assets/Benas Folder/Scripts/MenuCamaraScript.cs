using UnityEngine;

public class MenuCamaraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += 10 *Time.deltaTime;
        transform.localEulerAngles = currentRotation;
    }
}
