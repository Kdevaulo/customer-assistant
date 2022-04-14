using UnityEngine;

namespace Dummy
{
    public class ItemSwitcher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _itemSprite;
        [SerializeField] private Item[] _items;
        private int _itemsCount = 0;
        private const int EmptyItemIndex = -1;

        private void Start()
        {
            SetEmptyItem();
        }

        public void NextItem()
        {
            _itemsCount++;
            if (_itemsCount > _items.Length - 1)
            {
                _itemsCount = 0;
            }
            _itemSprite.sprite = _items[_itemsCount].ItemImage;
        }

        public void PrevItem()
        {
            _itemsCount--;
            if (_itemsCount <= EmptyItemIndex)
            {
                SetEmptyItem();
            }
            else
            {
                _itemSprite.sprite = _items[_itemsCount].ItemImage;
            }
        }

        private void SetEmptyItem()
        {
            _itemSprite.sprite = null;
            _itemsCount = EmptyItemIndex;
        }
    }
}