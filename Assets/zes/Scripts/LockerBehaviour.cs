using System;
using System.Collections.Generic;
using UnityEngine;

public class LockerBehaviour : MonoBehaviour
{
    List<LockerDigit> digits = new List<LockerDigit>();
    public event Action OnUnlock;
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var digit = child.GetComponent<LockerDigit>();
            if (digit != null)
            {
                digit.OnDigitChanged += Digit_OnDigitChanged;
                digits.Add(digit);
            }
        }
    }

    private void Digit_OnDigitChanged()
    {
        if (CheckCombination())
        {
            Debug.Log("Locker Unlocked!");
            OnUnlock?.Invoke();
        }
    }
    
    private bool CheckCombination()
    {
        foreach (LockerDigit digit in digits)
        {
            if (!digit.IsValueCorrect()) return false;
        }

        return true;
    }
}
