using System.Collections;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    public LayerMask collisionMask;
    public float raycastDistance;
    bool isRotating = false;

    void CheckCollisions()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("RCheck");
                if (hit.transform.gameObject.GetComponent<Item>().item.CanRotate())
                {
                    if(!isRotating) StartCoroutine(Rotate90Y(hit.transform));
                    Debug.Log("rotate");
                }
                if (hit.transform.gameObject.GetComponent<Item>().item.GetPick())
                {
                    GameManager.Instance.listInventory.Add(hit.transform.gameObject.GetComponent<Item>().item);
                    Destroy(hit.transform.gameObject);
                    Debug.Log("picked");
                }

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



}
