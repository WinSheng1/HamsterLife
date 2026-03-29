using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    [SerializeField] private GameObject hotbarPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] public int slotCount = 5;
    private Key[] hotbarKeys;

    private void Awake()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();

        hotbarKeys = new Key[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            hotbarKeys[i] = Key.Digit1 + i; // Assign keys 1-5 to the hotbar slots
        }
    }

    void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                UseHotbarItem(i);
            }
        }
    }

    private void UseHotbarItem(int index)
    {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if (slot != null && slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            if (item != null)
            {
                item.UseItem();
            }
        }
    }

    public List<InventorySaveData> GetHotbarItems()
    {
        List<InventorySaveData> hotbarItems = new List<InventorySaveData>();
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                hotbarItems.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return hotbarItems;
    }

    public void SetHotbarItems(List<InventorySaveData> inventorySaveData)
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform);
        }

        foreach (InventorySaveData data in inventorySaveData)
        {
            Slot slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
            GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
            if (itemPrefab != null)
            {
                GameObject item = Instantiate(itemPrefab, slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }
}
