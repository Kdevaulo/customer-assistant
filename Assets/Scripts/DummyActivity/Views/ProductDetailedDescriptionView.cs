using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

using DummyActivity.Configs;

namespace DummyActivity.Views
{
    public class ProductDetailedDescriptionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _itemImage;

        [SerializeField] private GameObject _descriptionMenu;

        [SerializeField] private TextMeshProUGUI _nameText;

        [SerializeField] private TextLinkView _descriptionText;

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
            _nameText.text = $"<align=center><color=#F7FCFB><size=130%>{item.Name}\n";
            _nameText.text += $"<align=center><size=100%><color=#7C8483>Цена: {item.Price}\n";

            if (item.Shop.GetShopURLString() != null)
            {
                _descriptionText.LinkText.text = $"<align=left><color=#F7FCFB><size=120%>Производитель: {item.Shop.GetShopURLString()}\n";
            }
            else
            {
                _descriptionText.LinkText.text = $"<align=left><color=#F7FCFB><size=120%>Производитель: {item.Shop.Name}\n";
            }

            string delivery = item.Delivery ? "Возможна доставка" : "Нет доставки";

            _descriptionText.LinkText.text += $"<color=#7C8483><size=100%>{delivery}\n";

            _descriptionText.LinkText.text += $"<color=#F7FCFB><size=120%>Описание\n";

            _descriptionText.LinkText.text += $"<color=#7C8483><size=100%>Размер модели: {item.GetSizeString()}\n";
            _descriptionText.LinkText.text += $"<color=#7C8483><size=100%>Цвет: {item.Color}\n";
        }
    }
}