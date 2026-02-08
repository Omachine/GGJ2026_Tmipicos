using System;
using System.Collections;
using UnityEngine;

public class LockerDigit : MonoBehaviour, IInteractable
{
    public event Action OnDigitChanged;
    
    private float baseRotation;
    public int Value;
    [SerializeField] int CorrectValue;
    private float rotationSpeed = 114f;
    
    private void Awake()
    {
        baseRotation = transform.localEulerAngles.x;
    }

    public void Interact()
    {
        Value = (Value + 1) % 10;
        Debug.Log(Value);
        Debug.Log(Value * 36f);
        OnDigitChanged?.Invoke();
        
        float targetAngle = baseRotation + Value * 36f;
        Debug.Log(targetAngle);
        transform.localRotation = Quaternion.Euler(targetAngle, 0, 0);
        Debug.Log(transform.rotation);
    }

    public bool IsValueCorrect() => CorrectValue == Value;
}
