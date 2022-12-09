using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class TextLinkView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _linkText;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        int linkIndex =
            TMP_TextUtilities.FindIntersectingLink(_linkText, eventData.position, eventData.enterEventCamera);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = _linkText.textInfo.linkInfo[linkIndex];

            Application.OpenURL(linkInfo.GetLinkID());
        }
    }

    public TextMeshProUGUI LinkText => _linkText;
}