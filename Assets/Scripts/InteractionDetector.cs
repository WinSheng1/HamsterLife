using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionPrompt;

    void Start()
    {
        interactionPrompt.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && interactableInRange != null && interactableInRange.CanInteract())
        {
            interactableInRange.Interact();
            if (!interactableInRange.CanInteract())
            {
                interactableInRange = null;
                interactionPrompt.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() == interactableInRange)
        {
            interactableInRange = null;
            interactionPrompt.SetActive(false);
        }
    }
}
