using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portrait;
    private DialogueData currentDialogueData;
    private int currentLineIndex;
    private bool isTyping, isDialogueActive;
    private bool skipNextInput; // Skip input for one frame after dialogue starts to prevent immediate skipping of first line

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Keyboard.current[Key.E].wasPressedThisFrame && !skipNextInput)
        {
            if (isTyping)
            {
                // Skip typing animation, show full line
                StopAllCoroutines();
                dialogueText.text = currentDialogueData.dialogueLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                // Move to next line
                NextLine();
            }
        }

        skipNextInput = false;
    }

    public void PlayDialogue(DialogueData dialogueData)
    {
        if (isDialogueActive) return;

        currentDialogueData = dialogueData;
        currentLineIndex = 0;
        isDialogueActive = true;
        skipNextInput = true;

        dialoguePanel.SetActive(true);
        nameText.text = currentDialogueData.npcName;
        portrait.sprite = currentDialogueData.npcPortrait;

        PauseController.TogglePause(true);

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        string line = currentDialogueData.dialogueLines[currentLineIndex];
        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentDialogueData.typingSpeed);
        }
        isTyping = false;
    }

    private void NextLine()
    {
        currentLineIndex++;
        
        if (currentLineIndex < currentDialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        PauseController.TogglePause(false);
    }
}
