using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ItemPopupUIController : MonoBehaviour
{
    public static ItemPopupUIController Instance { get; private set; }

    [SerializeField] private GameObject itemPopupPrefab;
    [SerializeField] private int maxPopups = 5;
    [SerializeField] private float popupDuration = 3f;

    private readonly Queue<GameObject> activePopups = new();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowItemPopup(string itemName, Sprite itemIcon)
    {
        GameObject popup = Instantiate(itemPopupPrefab, transform);
        popup.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = itemName;

        Image itemImage = popup.transform.Find("ItemIcon").GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = itemIcon;
        }

        activePopups.Enqueue(popup);
        if (activePopups.Count > maxPopups)
        {
            GameObject oldestPopup = activePopups.Dequeue();
            Destroy(oldestPopup);
        }

        StartCoroutine(FadeOutAndDestroy(popup));
    }

    public IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);
        if (popup == null) yield break; 

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            if (popup == null) yield break;
            canvasGroup.alpha = 1f - t;
            yield return null;
        }

        Destroy(popup);
    }
}
