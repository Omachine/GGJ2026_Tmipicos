using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    public LayerMask collisionMask;
    public float raycastDistance;
    bool isRotating = false;
    bool isLidOpen = false;
    public Image fadeImage;
    private float fadeDuration;
    private float degrees;
    private float totalDegrees;
    private float rotation;

    [SerializeField] private GameObject keyBook;
    [SerializeField] private TMP_Text tooltip;
    [SerializeField] private Canvas canvas;



    void Start()
    {

        degrees = 0.5f;
        rotation = 45f;
        totalDegrees = 0;
        fadeDuration = 0.12f;
        //RotateDoll();
    }
    void CheckCollisions()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            tooltip.gameObject.SetActive(true);
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
                        if (hit.transform.gameObject.CompareTag("Mask1"))
                        {
                            canvas.transform.Find("Inventory/Mask1/Image").gameObject.SetActive(true);
                            PlayerController.hasMask = true;
                        }
                        if (hit.transform.gameObject.CompareTag("Mask2"))
                        {
                            canvas.transform.Find("Inventory/Mask2/Image").gameObject.SetActive(true);
                            PlayerController.hasMask2 = true;
                        }
                        if (hit.transform.gameObject.CompareTag("KeyBook"))
                        {
                            canvas.transform.Find("Key/Image").gameObject.SetActive(true);
                        }

                        GameManager.Instance.listInventory.Add(hit.transform.gameObject.GetComponent<Item>().item);
                        Destroy(hit.transform.gameObject);

                        //falta o resto

                    }
                    if (hit.transform.gameObject.GetComponent<Item>().item.IsClock())
                    {
                        if (!isRotating) StartCoroutine(RotateClock(hit.transform, -30f, 0f));

                    }
                }

                if (hit.transform.tag == "DollMask") PlayerController.isInDoll = true;

                if (hit.transform.tag == "KeyBook")
                {
                    StartCoroutine(RotateBook(-1, hit));
                    //StartCoroutine(Fade(1, 0));
                    
                }

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
            }
        }
        else
        {
            tooltip.gameObject.SetActive(false);
        }
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

    IEnumerator RotateBook(int multiplier, RaycastHit hit)
    {
        while (totalDegrees < rotation)
        {
            hit.transform.Rotate(degrees * multiplier, 0, 0);
            totalDegrees += degrees;

            yield return new WaitForSeconds(0.01f);

        }
        //this.gameObject.transform.Rotate(80 * multiplier, 0, 0);
        //StartCoroutine(Fade(0, 1));
        keyBook.SetActive(true);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        Color color = fadeImage.color;
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            float t = timer / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }



}
