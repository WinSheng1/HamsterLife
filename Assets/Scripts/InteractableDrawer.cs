using UnityEngine;

public class InteractableDrawer : MonoBehaviour, IInteractable
{
    public bool isInteracted { get; private set; }
    public string drawerID { get; private set; }
    public GameObject itemPrefab; // Item prefab to spawn when the drawer is interacted with
    public Sprite interactedSprite;
    public DialogueData drawerDialogue;

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
        InteractDrawer();
    }

    private void InteractDrawer()
    {
        setInteracted(true);
        
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

        if (drawerDialogue != null)
        {
            DialogueController dialogueController = FindFirstObjectByType<DialogueController>();
            if (dialogueController != null)
            {
                dialogueController.PlayDialogue(drawerDialogue);
            }
        }
    }

    public void setInteracted(bool interacted)
    {
        isInteracted = interacted;
        if (interacted && interactedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = interactedSprite;
        }
    }
}
