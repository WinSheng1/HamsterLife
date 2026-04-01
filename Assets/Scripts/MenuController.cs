using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    
    void Start()
    {
        menuPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (PauseController.IsPaused && !menuPanel.activeSelf)
            {
                return; // Don't allow opening the menu if the game is paused and the menu is not already open
            }
            menuPanel.SetActive(!menuPanel.activeSelf);
            PauseController.TogglePause(menuPanel.activeSelf);
        }
    }
}
