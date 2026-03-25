using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string itemName;
    private bool hasBeenPickedUp = false;

    public virtual void PickUp()
    {
        if (hasBeenPickedUp) return;
        hasBeenPickedUp = true;

        Sprite itemIcon = GetComponent<SpriteRenderer>()?.sprite;
        if (ItemPopupUIController.Instance != null)
        {
            ItemPopupUIController.Instance.ShowItemPopup(itemName, itemIcon);
        }
    }
}
