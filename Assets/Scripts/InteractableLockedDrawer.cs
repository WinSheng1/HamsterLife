using UnityEngine;

public class InteractableLockedDrawer : InteractableItemDrawer
{
    public DialogueData lockedDialogue;

    public override void Interact()
    {
        if (!CanInteract()) 
        {
            return;
        }
        
        if (HasKeySelected())
        {
            base.Interact();
        }
        else
        {
            PlayLockedDialogue();
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
