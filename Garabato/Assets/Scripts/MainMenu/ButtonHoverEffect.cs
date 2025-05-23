using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI buttonText;
    public Color hoverColor = Color.yellow;
    private Color originalColor;

    void Start()
    {
        if (buttonText != null)
            originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = originalColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = hoverColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = originalColor;
    }
}
