using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields
    private TextMeshProUGUI buttonText;

    public Color normalColor = Color.white;
    public Color highlightColor = Color.black;
    #endregion Fields

    #region MonoBehaviour
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = normalColor;
    }

    void OnDisable()
    {
        // If buttonText is null don't run this code
        if (!buttonText) return;

        // Stop the buttons from being stuck on highlight after deactivation
        buttonText.color = normalColor;
    }
    #endregion MonoBehaviour

    #region ButtonHandler
    public void OnPointerEnter(PointerEventData eventData)
    {
        // If buttonText is null don't run this code
        if (!buttonText) return;

        buttonText.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If buttonText is null don't run this code
        if (!buttonText) return;

        buttonText.color = normalColor;
    }
    #endregion ButtonHandler
}
