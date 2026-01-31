using UnityEngine;

public class TestInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("TestInteract: Interacted with " + gameObject.name);
    }
}
