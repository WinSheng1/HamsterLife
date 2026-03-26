using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private CameraBoundsManager boundsManager;
    private InventoryController inventoryController;

    void Start()
    {
        boundsManager = FindFirstObjectByType<CameraBoundsManager>();
        saveLocation = Path.Combine(Application.persistentDataPath, "savefile.json");
        inventoryController = FindAnyObjectByType<InventoryController>();

        LoadGame();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        data.currentRoom = boundsManager.GetCurrentRoom();
        data.inventorySaveData = inventoryController.SaveInventory();

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

            inventoryController.LoadInventory(data.inventorySaveData);

            Debug.Log("Game Loaded from " + saveLocation);
        }
        else
        {
            SaveGame();

            inventoryController.LoadInventory(new List<InventorySaveData>());
        }
    }
}
