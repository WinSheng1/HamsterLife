using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableGiftbox : InteractableDrawer
{
    [SerializeField] private string cutsceneSceneName;

    public override void Interact()
    {
        if (!CanInteract())
        {
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
}