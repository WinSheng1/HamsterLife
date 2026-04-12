using UnityEngine;
using System.Collections;

public class InteractableRandomItem : InteractableDrawer
{
    [SerializeField] private GameObject[] items;

    public override void Interact()
    {
        if (!CanInteract()) 
        {
            return;
        }
        
        InteractDrawer();
    }

    private void InteractDrawer()
    {
        SetInteracted(true);
        
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        
        if (selectedItem != null)
        {
            InventoryController inventoryController = FindFirstObjectByType<InventoryController>();
            if (inventoryController != null)
            {
                bool added = inventoryController.AddItem(selectedItem);
                if (added)
                {
                    Item item = selectedItem.GetComponent<Item>();
                    item.PickUp();
                }
            }
        }

        SetInteracted(false);
    }
}