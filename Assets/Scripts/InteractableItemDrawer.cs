using UnityEngine;

public class InteractableItemDrawer : InteractableDrawer
{
    public GameObject itemPrefab;
    public DialogueData dialogue;

    public override void Interact()
    {
        if (!CanInteract()) 
        {
            return;
        }
        
        InteractDrawer();
    }

    protected virtual void InteractDrawer()
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

        if (dialogue != null)
        {
            DialogueController dialogueController = FindFirstObjectByType<DialogueController>();
            if (dialogueController != null)
            {
                dialogueController.PlayDialogue(dialogue);
            }
        }
    }
}
