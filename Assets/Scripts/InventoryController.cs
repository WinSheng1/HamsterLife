using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int slotCount;
    [SerializeField] private DialogueData inventoryFullDialogue;

    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        
        if (inventoryFullDialogue != null)
        {
            DialogueController dialogueController = FindFirstObjectByType<DialogueController>();
            if (dialogueController != null)
            {
                dialogueController.PlayDialogue(inventoryFullDialogue);
            }
        }
        
        return false;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> inventoryItems = new List<InventorySaveData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                inventoryItems.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return inventoryItems;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach (InventorySaveData data in inventorySaveData)
        {
            Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
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
