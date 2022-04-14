using System;
using UnityEngine;

namespace Dummy
{
    public class ItemModel : MonoBehaviour
    {
        [SerializeField] private Item[] _items;
        private int _itemIndex;
        public event Action ItemChanged;
        public event Action<bool> InfoClick;
        public Sprite ItemImage { get; private set; }

        public void NextItem()
        {
            if (_itemIndex < _items.Length - 1)
            {
                _itemIndex += 1;
                ItemImage = _items[_itemIndex].ItemImage;
                ItemChanged?.Invoke();
            }
        }

        public void PreviousItem()
        {
            _itemIndex -= 1;
            if (_itemIndex > -1)
            {
                ItemImage = _items[_itemIndex].ItemImage;
            }
            else if (_itemIndex <= -1)
            {
                SetEmptyItem();
            }
            ItemChanged?.Invoke();
        }

        public string GetDescriptionText()
        {
            if (_itemIndex == -1)
                return null;
            string descriptionText = $"{_items[_itemIndex].ItemName}\n{_items[_itemIndex].ShopName}\n{_items[_itemIndex].ItemPrice}";
            return descriptionText;
        }

        public void ShowInfo(bool clicked)
        {
            if (ItemImage != null)
            {
                InfoClick?.Invoke(clicked);
            }
        }

        private void SetEmptyItem()
        {
            _itemIndex = -1;
            ItemImage = null;
        }

        public void Init()
        {
            _itemIndex = 0;
            ItemImage = _items[_itemIndex].ItemImage;
        }
    }
}