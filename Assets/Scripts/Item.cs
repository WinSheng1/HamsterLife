using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string itemName;
    private bool isBeingCollected = false;

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
