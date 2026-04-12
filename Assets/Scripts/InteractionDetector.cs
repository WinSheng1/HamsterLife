using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionPrompt;
    private bool justInteracted = false; // Flag to track if interaction happened this frame

    void Start()
    {
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        justInteracted = false; // Reset at start of frame
    }

    public bool JustInteracted => justInteracted;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && interactableInRange != null && interactableInRange.CanInteract())
        {
            interactableInRange.Interact();
            justInteracted = true;
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
