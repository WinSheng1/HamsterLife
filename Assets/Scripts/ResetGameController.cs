using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameController : MonoBehaviour
{
    public void ResetGame()
    {
        string saveLocation = Path.Combine(Application.persistentDataPath, "savefile.json");
        
        if (File.Exists(saveLocation))
        {
            File.Delete(saveLocation);
            Debug.Log("Save file deleted: " + saveLocation);
        }

        PauseController.TogglePause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
