using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LockerBehaviour locker;
    float initialAngle;
    public bool reverseOrientation;

    private void Start()
    {
        initialAngle = transform.eulerAngles.y;
        locker.OnUnlock += OpenDoor;
    }

    private void OpenDoor()
    {
        StartCoroutine(OpenAnimation());
    }

    IEnumerator OpenAnimation()
    {
        float time = 0;
        
        while (time < 2f)
        {
            time += Time.deltaTime;
            float angle = Mathf.Lerp(0f, reverseOrientation ? -90f : 90f, time / 2f);
            transform.rotation = Quaternion.Euler(0f, angle + initialAngle, 0f);
            yield return null;
        }
    }
}
