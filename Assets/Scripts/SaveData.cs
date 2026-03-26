using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public RoomId currentRoom;
    public List<InventorySaveData> inventorySaveData;
    public List<DrawerSaveData> drawersSaveData;
}

[System.Serializable]
public class DrawerSaveData
{
    public string drawerID;
    public bool isInteracted;
}
