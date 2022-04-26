using System;
using UnityEngine;

namespace Dummy
{
    public class ItemModel : MonoBehaviour
    {
        [SerializeField] private Item[] _items;
        private int _itemIndex;
        private Sprite _itemImage;
        public Sprite ItemImage => _itemImage;
        public int ItemIndex => _itemIndex;
        public event Action ItemChanged;
        public event Action<bool> InfoClick;

        public void NextItem()
        {
            if (_itemIndex < _items.Length - 1)
            {
                _itemIndex += 1;
                _itemImage = _items[_itemIndex].Image;
                ItemChanged?.Invoke();
            }
        }

        public void PreviousItem()
        {
            _itemIndex -= 1;
            if (_itemIndex > -1)
            {
                _itemImage = _items[_itemIndex].Image;
            }
            else if (_itemIndex <= -1)
            {
                SetEmptyItem();
            }
            ItemChanged?.Invoke();
        }

        public Item GetCurrentItem()
        {
            if (_itemIndex == -1)
                return null;

            return _items[_itemIndex];
        }

        public void ShowInfo(bool clicked)
        {
            if (ItemImage == null)
                return;

            InfoClick?.Invoke(clicked);
        }

        private void SetEmptyItem()
        {
            _itemIndex = -1;
            _itemImage = null;
        }

        public void Init()
        {
            _itemIndex = 0;
            _itemImage = _items[_itemIndex].Image;
        }
    }
}