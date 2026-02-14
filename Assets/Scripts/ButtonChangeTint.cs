using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeTint : MonoBehaviour
{
    [Range(0f, 1.15f)]
    [SerializeField] private float hoverMultAmount = 0.85f;
    [Range(0f, 0.75f)]
    [SerializeField] private float pressMultAmount = 0.75f;
    private TextMeshProUGUI buttonText;
    private Image image;
    private Color baseImageColor;
    private Color baseTextColor;
    private bool isSelected = false;

    private void Awake()
    {
        image = GetComponent<Image>();
        baseImageColor = image.color;
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        baseTextColor = buttonText.color;
    }

    // Only used for tabs to retrieve the base color (gray or white) from the TabController
    public void SetBaseColors(Color newImageBase)
    {
        baseImageColor = newImageBase;
    }

    // Only used for tabs to set whether the tab is selected or not, which will disable hover and press effects
    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    public void OnHoverEnter()
    {
        if (isSelected)
            return;
        image.color = MultiplyKeepAlpha(baseImageColor, hoverMultAmount);
        buttonText.color = MultiplyKeepAlpha(baseTextColor, hoverMultAmount);
    }

    public void OnHoverExit()
    {
        if (isSelected)
            return;
        RestoreBaseColors();
    }

    public void OnPress()
    {
        if (isSelected)
            return;
        image.color = MultiplyKeepAlpha(baseImageColor, pressMultAmount);
        buttonText.color = MultiplyKeepAlpha(baseTextColor, pressMultAmount);
    }

    public void OnRelease()
    {
        if (isSelected)
            return;
        RestoreBaseColors();
    }

    private void RestoreBaseColors()
    {
        image.color = baseImageColor;
        buttonText.color = baseTextColor;
    }

    private Color MultiplyKeepAlpha(Color color, float multiplier)
    {
        return new Color(color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a);
    }
}
