using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dummy
{
    public class ItemDetailedDescriptionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _descriptionMenu;
        public event Action OpenDescription;

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenDescription?.Invoke();
            _descriptionMenu.SetActive(true);
        }

        public void SetIcon(Sprite image)
        {
            _itemImage.sprite = image;
            _itemImage.SetNativeSize();
        }

        public void SetDescriptionText(string text)
        {
            _descriptionText.text = text;
        }
    }
}