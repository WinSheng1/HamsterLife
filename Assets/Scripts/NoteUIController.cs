using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteUIController : MonoBehaviour
{
    public static NoteUIController Instance { get; private set; }
    
    [SerializeField] private TMP_Text noteText;
    private GameObject hotbarPanel;
    private GameObject menuPanel;
    
    private bool isNoteOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        // Get references to the panels we need to hide
        hotbarPanel = FindFirstObjectByType<HotbarController>().gameObject;
        menuPanel = FindFirstObjectByType<MenuController>().gameObject.transform.Find("Menu").gameObject;
        
        // Ensure note is hidden on start
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNoteOpen && Keyboard.current[Key.E].wasPressedThisFrame)
        {
            CloseNote();
        }
    }

    public void ShowNote(string message)
    {
        noteText.text = message;
        gameObject.SetActive(true);
        isNoteOpen = true;
        
        PauseController.TogglePause(true);
        hotbarPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

    private void CloseNote()
    {
        gameObject.SetActive(false);
        isNoteOpen = false;
        
        PauseController.TogglePause(false);
        hotbarPanel.SetActive(true);
    }
}