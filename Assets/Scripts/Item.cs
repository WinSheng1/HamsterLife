using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string itemName;
    public bool canConsume = false;
    private bool isBeingCollected = false;

    public virtual void UseItem()
    {
        Debug.Log("Using item: " + itemName);
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<SpriteRenderer>()?.sprite;
        if (ItemPopupUIController.Instance != null)
        {
            ItemPopupUIController.Instance.ShowItemPopup(itemName, itemIcon);
        }
    }

    public bool IsBeingCollected()
    {
        return isBeingCollected;
    }

    public void SetBeingCollected(bool value)
    {
        isBeingCollected = value;
    }
}
