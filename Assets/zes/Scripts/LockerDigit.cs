using System;
using System.Collections;
using UnityEngine;

public class LockerDigit : MonoBehaviour, IInteractable
{
    public event Action OnDigitChanged;
    
    private float baseRotation;
    private int Value;
    [SerializeField] int CorrectValue;
    private float rotationSpeed = 114f;

    private Coroutine rotateCoroutine;
    
    private void Awake()
    {
        baseRotation = transform.eulerAngles.y;
    }

    public void Interact()
    {
        Value = (Value + 1) % 10;
        OnDigitChanged?.Invoke();
        StartCoroutine(Rotate());
    }

    public bool IsValueCorrect() => CorrectValue == Value;

    IEnumerator Rotate()
    {
        float currentRotation = transform.eulerAngles.y;
        float targetAngle = baseRotation + Value * 36f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetAngle)) > 0.1f)
        {
            currentRotation = Mathf.MoveTowardsAngle(currentRotation, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotation, transform.eulerAngles.z);
            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
        rotateCoroutine = null;
    }
}
