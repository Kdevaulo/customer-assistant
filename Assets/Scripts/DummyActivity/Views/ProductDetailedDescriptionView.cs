using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using DummyActivity.Configs;

namespace DummyActivity.Views
{
    public class ProductDetailedDescriptionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text _descriptionText;

        [SerializeField] private Image _itemImage;

        [SerializeField] private GameObject _descriptionMenu;

        public event Action OpenDescription;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OpenDescription?.Invoke();

            _descriptionMenu.SetActive(true);
        }

        public void SetIcon(Sprite image)
        {
            _itemImage.sprite = image;

            _itemImage.SetNativeSize();
        }

        public void SetDescriptionText(Product item)
        {
            _descriptionText.text = $"{item.Name}\n";
            _descriptionText.text += $"Цена: {item.Price}\n";
            _descriptionText.text += $"Производитель: {item.Shop.Name}";
        }
    }
}