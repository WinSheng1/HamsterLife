using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeTint : MonoBehaviour
{
    [Range(0f, 1.15f)]
    [SerializeField] private float hoverMultAmount = 0.85f;
    [Range(0f, 1f)]
    [SerializeField] private float pressMultAmount = 0.75f;
    private TextMeshProUGUI buttonText;
    private Image image;
    private Color baseImageColor;
    private Color baseTextColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        baseImageColor = image.color;
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        baseTextColor = buttonText.color;
    }

    public void SetBaseColors(Color newImageBase)
    {
        baseImageColor = newImageBase;
    }

    public void OnHoverEnter()
    {
        image.color = MultiplyKeepAlpha(baseImageColor, hoverMultAmount);
        buttonText.color = MultiplyKeepAlpha(baseTextColor, hoverMultAmount);
    }

    public void OnHoverExit()
    {
        RestoreBaseColors();
    }

    public void OnPress()
    {
        image.color = MultiplyKeepAlpha(baseImageColor, pressMultAmount);
        buttonText.color = MultiplyKeepAlpha(baseTextColor, pressMultAmount);
    }

    public void OnRelease()
    {
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
