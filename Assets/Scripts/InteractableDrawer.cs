using Unity.VisualScripting;
using UnityEngine;

public class InteractableDrawer : MonoBehaviour, IInteractable
{
    public bool isInteracted { get; private set; }
    public string drawerID { get; private set; }
    public GameObject itemPrefab; // Item prefab to spawn when the drawer is interacted with
    public Sprite interactedSprite;

    void Start()
    {
        drawerID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        return !isInteracted;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
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
