using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsPaused { get; private set; } = false;
    public static void TogglePause(bool pause)
    {
        IsPaused = pause;
    }
}
