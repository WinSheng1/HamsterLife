using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private CameraBoundsManager boundsManager;

    void Start()
    {
        boundsManager = FindFirstObjectByType<CameraBoundsManager>();
        saveLocation = Path.Combine(Application.persistentDataPath, "savefile.json");
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        data.currentRoom = boundsManager.GetCurrentRoom();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveLocation, json);
        Debug.Log("Game Saved to " + saveLocation);
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string json = File.ReadAllText(saveLocation);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = data.playerPosition;

            boundsManager.SetRoom(data.currentRoom);

            Debug.Log("Game Loaded from " + saveLocation);
        }
        else
        {
            SaveGame();
        }
    }
}
