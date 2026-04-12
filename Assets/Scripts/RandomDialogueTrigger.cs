using UnityEngine;
using UnityEngine.InputSystem;

public class RandomDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData[] dialogues;
    private DialogueController dialogueController;

    void Start()
    {
        dialogueController = FindFirstObjectByType<DialogueController>();
    }

    void Update()
    {
        if (Keyboard.current[Key.F].wasPressedThisFrame)
        {
            if (dialogues.Length > 0)
            {
                int randomIndex = Random.Range(0, dialogues.Length);
                dialogueController.PlayDialogue(dialogues[randomIndex]);
            }
        }
    }
}