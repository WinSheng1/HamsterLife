using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // Store the original parent to return to it later
        transform.SetParent(transform.root); // Move to root to ensure it's on top of other UI elements
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // Slot where the item is dropped
        Slot originalSlot = originalParent.GetComponent<Slot>(); // Original slot of the dragged item

        if (dropSlot == null)
        {
            dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>(); // Check parent if the pointer is over a child element
        }
        
        if (dropSlot != null)
        {
            // If the slot already has an item, swap them
            if (dropSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(originalParent);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }
            // Move the dragged item to the new slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject; 
        }
        else
        {
            transform.SetParent(originalParent); // Return to the original parent if dropped outside a valid slot
        }
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Reset position to center of the slot
    }
}
