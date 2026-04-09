using UnityEngine;

public class InteractableItemDrawer : MonoBehaviour, IInteractable
{
    public bool isInteracted { get; private set; }
    public string drawerID { get; private set; }
    public GameObject itemPrefab;
    public Sprite interactedSprite;
    public DialogueData noteDialogue;
    public DialogueData lockedDialogue;

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
        if (HasKeySelected())
        {
            InteractDrawer();
        }
        else
        {
            PlayLockedDialogue();
        }
    }

    private void InteractDrawer()
    {
        SetInteracted(true);
        
        if (itemPrefab != null)
        {
            InventoryController inventoryController = FindFirstObjectByType<InventoryController>();
            if (inventoryController != null)
            {
                bool added = inventoryController.AddItem(itemPrefab);
                if (added)
                {
                    Item item = itemPrefab.GetComponent<Item>();
                    item.PickUp();
                }
            }
        }

        if (noteDialogue != null)
        {
            DialogueController dialogueController = FindFirstObjectByType<DialogueController>();
            if (dialogueController != null)
            {
                dialogueController.PlayDialogue(noteDialogue);
            }
        }
    }

    public void SetInteracted(bool interacted)
    {
        isInteracted = interacted;
        if (interacted && interactedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = interactedSprite;
        }
    }

    private bool HasKeySelected()
    {
        HotbarController hotbarController = FindFirstObjectByType<HotbarController>();
        if (hotbarController == null) return false;
        
        int selectedSlot = hotbarController.GetCurrentSelectedSlot();
        if (selectedSlot < 0) return false;
        
        Slot slot = hotbarController.GetSlot(selectedSlot);
        if (slot?.currentItem == null) return false;
        
        Item item = slot.currentItem.GetComponent<Item>();
        return item != null && item.itemName == "Key";
    }

    private void PlayLockedDialogue()
    {
        if (lockedDialogue != null)
        {
            DialogueController dialogueController = FindFirstObjectByType<DialogueController>();
            if (dialogueController != null)
            {
                dialogueController.PlayDialogue(lockedDialogue);
            }
        }
    }
}
