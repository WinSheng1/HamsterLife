using UnityEngine;

public class InteractableDrawer : MonoBehaviour, IInteractable
{
    public bool isInteracted { get; private set; }
    public string drawerID { get; private set; }
    public Sprite interactedSprite;

    void Start()
    {
        drawerID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        if (isInteracted) return false;
        
        MenuController menuController = FindFirstObjectByType<MenuController>();
        if (menuController != null && menuController.IsMenuOpen)
            return false;
        
        return true;
    }

    public void Interact()
    {
        if (!CanInteract()) 
        {
            return;
        }
        SetInteracted(true);
    }

    public void SetInteracted(bool interacted)
    {
        isInteracted = interacted;
        if (interacted && interactedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = interactedSprite;
        }
    }
}