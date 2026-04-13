using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableGiftbox : InteractableDrawer
{
    [SerializeField] private string cutsceneSceneName;
    [SerializeField] private DialogueData noteRequiredDialogue;

    public override void Interact()
    {
        if (!CanInteract())
        {
            return;
        }

        if (HasNoteSelected())
        {
            return;
        }

        // Check if the locked drawer has been interacted with
        InteractableLockedDrawer lockedDrawer = FindFirstObjectByType<InteractableLockedDrawer>();
        if (lockedDrawer != null && !lockedDrawer.isInteracted)
        {
            PlayNoteRequiredDialogue();
            return;
        }

        SetInteracted(true);
        
        SaveController saveController = FindFirstObjectByType<SaveController>();
        if (saveController != null)
        {
            saveController.SaveGame();
        }
        
        if (!string.IsNullOrEmpty(cutsceneSceneName))
        {
            SceneManager.LoadScene(cutsceneSceneName);
        }
    }

    private bool HasNoteSelected()
    {
        HotbarController hotbarController = FindFirstObjectByType<HotbarController>();
        if (hotbarController == null) return false;
        
        int selectedSlot = hotbarController.GetCurrentSelectedSlot();
        if (selectedSlot < 0) return false;
        
        Slot slot = hotbarController.GetSlot(selectedSlot);
        if (slot?.currentItem == null) return false;
        
        Item item = slot.currentItem.GetComponent<Item>();
        return item != null && item.itemName == "Note";
    }

    private void PlayNoteRequiredDialogue()
    {
        if (noteRequiredDialogue != null)
        {
            DialogueController dialogueController = FindFirstObjectByType<DialogueController>();
            if (dialogueController != null)
            {
                dialogueController.PlayDialogue(noteRequiredDialogue);
            }
        }
    }
}