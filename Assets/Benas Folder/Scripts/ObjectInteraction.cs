using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public LayerMask collisionMask;
    public float raycastDistance;
    bool isRotating = false;
    bool isLidOpen = false;

    void CheckCollisions()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.transform.gameObject.GetComponent<Item>() != null)
                {         
                    if (hit.transform.gameObject.GetComponent<Item>().item.CanRotate())
                    {
                        if(!isRotating) StartCoroutine(Rotate90Y(hit.transform));

                    }
                    if (hit.transform.gameObject.GetComponent<Item>().item.GetPick())
                    {
                        GameManager.Instance.listInventory.Add(hit.transform.gameObject.GetComponent<Item>().item);
                        Destroy(hit.transform.gameObject);

                    }
                    if (hit.transform.gameObject.GetComponent<Item>().item.IsClock())
                    {
                        if (!isRotating) StartCoroutine(RotateClock(hit.transform, -30f, 0f));

                    }
                }

                if (hit.transform.tag == "DollMask") PlayerController.isInDoll = true;

                foreach (PickableObject i in GameManager.Instance.listInventory)
                {
                    if (i == null) continue;
                    if (i.GetObjToInteract() == null) continue;

                    if (hit.transform.gameObject.name == i.GetObjToInteract().name &&
                        hit.transform.gameObject.CompareTag("Chest") && !isLidOpen)
                    {
                        GameObject hinges = hit.transform.Find("ChestLid").gameObject;
                        StartCoroutine(RotateClock(hinges.transform, 0, 60f));
                        isLidOpen = true;
                    }
                }

                //if (hit.transform.gameObject.GetComponent<Item>().item.GetObjToInteract() == this.gameObject)
                //{

                //}



            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisions();
    }

    IEnumerator Rotate90Y(Transform target)
    {
        isRotating = true;
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);

        float t = 0f;
        float duration = 0.3f; // tempo da rotação (ajusta aqui)

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            target.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
        isRotating = false;
        target.rotation = endRotation; // garante que termina certinho
    }

    IEnumerator RotateClock(Transform target, float degreesZ, float degreesX) //-30
    {
        if (target.CompareTag("PointerHour")) GameManager.Instance.IncrementHour();
        if (target.CompareTag("PointerMinute")) GameManager.Instance.IncrementMinute();

        CheckTime(target);

        isRotating = true;
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(degreesX, 0f, degreesZ);

        float t = 0f;
        float duration = 0.2f; // tempo da rotação (ajusta aqui)

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            target.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
        isRotating = false;
        target.rotation = endRotation; // garante que termina certinho
    }

    void CheckTime(Transform target)
    {
        //Debug.Log(target.parent);
        //Debug.Log("currentMinute "+GameManager.Instance.currentMinute);
        //Debug.Log("targetMinute " +GameManager.Instance.targetMinute);
        //Debug.Log("currentHour " + GameManager.Instance.currentHour);
        //Debug.Log("targetHour "+GameManager.Instance.targetHour);

        if (GameManager.Instance.currentMinute == GameManager.Instance.targetMinute &&
            GameManager.Instance.currentHour == GameManager.Instance.targetHour)
        {
            
            target.parent.GetComponent<Animator>().SetTrigger("open");
        }
        return;
    }

    //IEnumerable OpenChest()
    //{
        

    //}



}
