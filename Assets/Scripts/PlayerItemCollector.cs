using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null && !item.IsBeingCollected())
            {
                item.SetBeingCollected(true);
                bool itemCollected = inventoryController.AddItem(collision.gameObject);
                if (itemCollected)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
                else
                {
                    item.SetBeingCollected(false);
                }
            }
        }
    }
}
