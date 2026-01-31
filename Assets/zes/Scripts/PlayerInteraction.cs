using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform camera;
    public float interactionRange = 3f;
    Ray ray;
    public bool Enabled = true;
    
    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
        
        ray = new Ray(camera.position, camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
