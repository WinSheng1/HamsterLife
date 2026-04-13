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