using System;

using CustomerAssistant.DummyActivity;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DummyActivity.Views
{
    public class ProductDetailedDescriptionView : MonoBehaviour, IPointerClickHandler
    {
        public event Action OpenDescription;

        [SerializeField] private Image _itemImage;

        [SerializeField] private GameObject _descriptionMenu;

        [SerializeField] private TextMeshProUGUI _nameText;

        [SerializeField] private TextLinkView _descriptionText;

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

            var urlString = GetShopURLString(item.Site, item.Shop_Name) ??
                            item.Shop_Name + "\n" + $"<color=#4d66cc><size=75%>{item.Address}";

            _descriptionText.LinkText.text = $"<align=left><color=#F7FCFB><size=120%>Производитель: {urlString}\n";

            string delivery = item.Delivery ? "Возможна доставка" : "Нет доставки";

            _descriptionText.LinkText.text += $"<color=#7C8483><size=100%>{delivery}\n";

            _descriptionText.LinkText.text += $"<color=#F7FCFB><size=120%>Описание\n";

            _descriptionText.LinkText.text += $"<color=#7C8483><size=100%>Размер модели: {item.Size}\n";
            _descriptionText.LinkText.text += $"<color=#7C8483><size=100%>Цвет: {item.Color}\n";
        }

        private string GetShopURLString(string site, string name)
        {
            if (!string.IsNullOrEmpty(site))
            {
                return string.Format("<#7f7fe5><u><link=\"{0}\">{1}</link></u></color>", site, name);
            }

            return null;
        }
    }
}