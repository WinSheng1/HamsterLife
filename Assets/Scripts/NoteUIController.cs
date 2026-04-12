using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteUIController : MonoBehaviour
{
    public static NoteUIController Instance { get; private set; }
    
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private GameObject notePopup;
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
        
        // Ensure notePopup is hidden on start
        if (notePopup == null)
            notePopup = transform.Find("NotePopup").gameObject;
        
        notePopup.SetActive(false);
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
        notePopup.SetActive(true);
        isNoteOpen = true;
        
        PauseController.TogglePause(true);
        hotbarPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

    private void CloseNote()
    {
        notePopup.SetActive(false);
        isNoteOpen = false;
        
        PauseController.TogglePause(false);
        hotbarPanel.SetActive(true);
    }
}