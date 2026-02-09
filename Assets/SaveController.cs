using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "savefile.json");

        LoadGame();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        data.mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.name;

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

            CinemachineConfiner2D confiner = FindFirstObjectByType<CinemachineConfiner2D>();
            confiner.BoundingShape2D = GameObject.Find(data.mapBoundary).GetComponent<PolygonCollider2D>();

            Debug.Log("Game Loaded from " + saveLocation);
        }
        else
        {
            SaveGame();
        }
    }
}
