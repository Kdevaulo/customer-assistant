using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SettingsActivity;
using SettingsActivity.Configs;
using SettingsActivity.Filters;
using SettingsActivity.Models;

namespace Dummy
{
    public class ItemModel : MonoBehaviour
    {
        [SerializeField] private List<Item> _items = new List<Item>();
        [SerializeField] private FilterConfig _filterConfig;
        private int _itemIndex;
        private Sprite _itemImage;
        private List<IFilter> _filters = new List<IFilter>();
        private List<Item> _filteredItems = new List<Item>();
        public Sprite ItemImage => _itemImage;
        public int ItemIndex => _itemIndex;
        public event Action ItemChanged;
        public event Action<bool> InfoClick;

        private void Awake()
        {
            // todo: call when user apply filters
            ApplyFilters();
        }

        public void NextItem()
        {
            if (_filteredItems == null)
                return;

            if (_itemIndex < _filteredItems.Count - 1)
            {
                _itemIndex += 1;
                _itemImage = _filteredItems[_itemIndex].Image;
                ItemChanged?.Invoke();
            }
        }

        public void PreviousItem()
        {
            if (_filteredItems == null)
                return;

            _itemIndex -= 1;
            if (_itemIndex > -1)
            {
                _itemImage = _filteredItems[_itemIndex].Image;
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

            return _filteredItems[_itemIndex];
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
            if (_filteredItems.Count == 0)
            {
                SetEmptyItem();
                Debug.LogWarning("Items doesn't match the filters, empty item was setted");
                return;
            }

            _itemIndex = 0;
            _itemImage = _filteredItems[_itemIndex].Image;
        }

        public void ApplyFilters()
        {
            _filteredItems = FilterItems();
        }

        private List<Item> FilterItems()
        {
            _filters = _filterConfig.GetFilters();

            FiltersModel filtersModel = new FiltersModel(_filters);
            List<IFilter> activeFilters = filtersModel.GetActiveFilters();
            List<bool> filterValues = new List<bool>();
            List<Item> filteredItems = new List<Item>();

            if (activeFilters.Count == 0)
            {
                return filteredItems = _items;
            }

            StringFilter color = null;
            ToggleFilter size = null;
            StringFilter material = null;
            BoolFilter sale = null;
            StringFilter shop = null;
            BoolFilter delivery = null;

            bool colorMatch = false;
            bool sizeMatch = false;
            bool materialMatch = false;
            bool saleMatch = false;
            bool shopMatch = false;
            bool deliveryMatch = false;

            foreach (Item item in _items)
            {
                foreach (IFilter filter in activeFilters)
                {
                    if (filter.Name == "Цвет")
                    {
                        color = (StringFilter)filter;
                        colorMatch = color.Value == item.Color;
                        filterValues.Add(colorMatch);
                    }
                    else if (filter.Name == "Размер")
                    {
                        size = (ToggleFilter)filter;
                        sizeMatch = CheckSize(size.Value, item);
                        filterValues.Add(sizeMatch);
                    }
                    else if (filter.Name == "Материал")
                    {
                        material = (StringFilter)filter;
                        materialMatch = material.Value == item.Material;
                        filterValues.Add(materialMatch);
                    }
                    else if (filter.Name == "Наличие скидки")
                    {
                        sale = (BoolFilter)filter;
                        saleMatch = sale.Value == item.Sale;
                        filterValues.Add(saleMatch);
                    }
                    else if (filter.Name == "Магазин")
                    {
                        shop = (StringFilter)filter;
                        shopMatch = shop.Value == item.ShopName;
                        filterValues.Add(shopMatch);
                    }
                    else if (filter.Name == "Возможность доставки")
                    {
                        delivery = (BoolFilter)filter;
                        deliveryMatch = delivery.Value == item.Delivery;
                        filterValues.Add(deliveryMatch);
                    }
                }

                if (!filteredItems.Contains(item) && filterValues.All(v => v == true))
                {
                    filteredItems.Add(item);
                }
                else
                {
                    Debug.LogWarning($"Item: {item.Name} – doesn't match the filters");
                }
                filterValues.Clear();
            }

            return filteredItems;
        }

        private bool CheckSize(List<StringBoolModel> sizeFilter, Item item)
        {
            List<StringBoolModel> itemSize = item.Size;
            List<bool> sizeValues = new List<bool>();
            bool correctSize = false;

            foreach (var s in sizeFilter)
            {
                foreach (var size in itemSize)
                {
                    correctSize = s.Value == true && size.Value == true && s.Text == size.Text;
                    sizeValues.Add(correctSize);
                }
            }

            bool sizeCheckResult = sizeValues.Any(v => v == true);
            return sizeCheckResult;
        }
    }
}