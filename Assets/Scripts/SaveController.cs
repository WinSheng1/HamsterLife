using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private CameraBoundsManager boundsManager;
    private InventoryController inventoryController;
    private HotbarController hotbarController;
    private InteractableDrawer[] interactableDrawers;
    private InteractableItemDrawer[] interactableItemDrawers;
    private InteractableLockedDrawer[] interactableLockedDrawers;

    void Start()
    {
        InitialiseComponents();
        LoadGame();
    }

    private void InitialiseComponents()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "savefile.json");
        boundsManager = FindFirstObjectByType<CameraBoundsManager>();
        inventoryController = FindAnyObjectByType<InventoryController>();
        hotbarController = FindAnyObjectByType<HotbarController>();
        interactableDrawers = FindObjectsByType<InteractableDrawer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        interactableItemDrawers = FindObjectsByType<InteractableItemDrawer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        interactableLockedDrawers = FindObjectsByType<InteractableLockedDrawer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        data.currentRoom = boundsManager.GetCurrentRoom();
        data.inventorySaveData = inventoryController.GetInventoryItems();
        data.hotbarSaveData = hotbarController.GetHotbarItems();
        data.drawersSaveData = GetDrawersState();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveLocation, json);
        Debug.Log("Game Saved to " + saveLocation);
    }

    private List<DrawerSaveData> GetDrawersState()
    {
        List<DrawerSaveData> drawersState = new List<DrawerSaveData>();
        foreach (InteractableDrawer drawer in interactableDrawers)
        {
            drawersState.Add(new DrawerSaveData
            {
                drawerID = drawer.drawerID,
                isInteracted = drawer.isInteracted
            });
        }
        foreach (InteractableItemDrawer drawer in interactableItemDrawers)
        {
            drawersState.Add(new DrawerSaveData
            {
                drawerID = drawer.drawerID,
                isInteracted = drawer.isInteracted
            });
        }
        foreach (InteractableLockedDrawer drawer in interactableLockedDrawers)
        {
            drawersState.Add(new DrawerSaveData
            {
                drawerID = drawer.drawerID,
                isInteracted = drawer.isInteracted
            });
        }
        return drawersState;
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

            inventoryController.SetInventoryItems(data.inventorySaveData);
            hotbarController.SetHotbarItems(data.hotbarSaveData);

            LoadDrawersState(data.drawersSaveData);

            Debug.Log("Game Loaded from " + saveLocation);
        }
        else
        {
            SaveGame();

            inventoryController.SetInventoryItems(new List<InventorySaveData>());
            hotbarController.SetHotbarItems(new List<InventorySaveData>());
        }
    }

    private void LoadDrawersState(List<DrawerSaveData> drawersState)
    {
        foreach (InteractableDrawer drawer in interactableDrawers)
        {
            DrawerSaveData drawerSaveData = drawersState.Find(d => d.drawerID == drawer.drawerID);
            if (drawerSaveData != null)
            {
                drawer.SetInteracted(drawerSaveData.isInteracted);
            }
        }
        foreach (InteractableItemDrawer drawer in interactableItemDrawers)
        {
            DrawerSaveData drawerSaveData = drawersState.Find(d => d.drawerID == drawer.drawerID);
            if (drawerSaveData != null)
            {
                drawer.SetInteracted(drawerSaveData.isInteracted);
            }
        }
        foreach (InteractableLockedDrawer drawer in interactableLockedDrawers)
        {
            DrawerSaveData drawerSaveData = drawersState.Find(d => d.drawerID == drawer.drawerID);
            if (drawerSaveData != null)
            {
                drawer.SetInteracted(drawerSaveData.isInteracted);
            }
        }
    }
}
