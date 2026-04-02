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
    private int currentSelectedSlot = -1; // -1 means no slot selected

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
        // Check for hotbar slot selection (1-5 keys)
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                SelectHotbarSlot(i);
            }
        }

        // Check for item usage (E key)
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            UseSelectedItem();
        }
    }

    private void SelectHotbarSlot(int index)
    {
        // If clicking the same slot, toggle it off (deselect)
        if (currentSelectedSlot == index)
        {
            Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
            if (slot.rimImage != null)
                slot.rimImage.SetActive(false);
            
            currentSelectedSlot = -1;
            Debug.Log("Deselected slot");
            return;
        }

        // Hide all rims
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.rimImage != null)
                slot.rimImage.SetActive(false);
        }

        // Show selected rim
        Slot selectedSlot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if (selectedSlot.rimImage != null)
            selectedSlot.rimImage.SetActive(true);

        currentSelectedSlot = index;
        Debug.Log("Selected hotbar slot: " + (index + 1));
    }

    private void UseSelectedItem()
    {
        // Don't use items if an interaction just occurred this frame
        InteractionDetector interactionDetector = FindFirstObjectByType<InteractionDetector>();
        if (interactionDetector != null && interactionDetector.JustInteracted)
        {
            return;
        }

        // Can only use an item if a slot is selected
        if (currentSelectedSlot < 0 || currentSelectedSlot >= slotCount)
        {
            return;
        }

        Slot slot = hotbarPanel.transform.GetChild(currentSelectedSlot).GetComponent<Slot>();
        if (slot != null && slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            if (item != null)
            {
                item.UseItem();
                // If item is consumable, destroy it
                if (item.canConsume)
                {
                    Destroy(slot.currentItem);
                    slot.currentItem = null;
                }
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
