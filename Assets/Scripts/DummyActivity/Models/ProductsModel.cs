using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using DummyActivity.Configs;

using SettingsActivity;
using SettingsActivity.Configs;
using SettingsActivity.Filters;
using SettingsActivity.Models;

namespace DummyActivity.Models
{
    public class ProductsModel
    {
        private List<Product> _products = new List<Product>();
        private int _itemIndex;
        private Sprite _itemImage;

        private FilterConfig _filterConfig;
        private List<IFilter> _filters = new List<IFilter>();
        private List<Product> _filteredItems = new List<Product>();

        public ProductsModel(List<Product> products, FilterConfig filterConfig)
        {
            _products = products;

            _filterConfig = filterConfig;

            Initialize();
        }

        public event Action ItemChanged;
        public event Action<bool> InfoClick;

        public Sprite ItemImage => _itemImage;

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

        public Product GetCurrentItem()
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

        private void Initialize()
        {
            ApplyFilters();

            if (_filteredItems.Count == 0)
            {
                SetEmptyItem();
                Debug.LogWarning("Items doesn't match the filters, empty item was setted");
                return;
            }

            _itemIndex = 0;
            _itemImage = _filteredItems[_itemIndex].Image;
        }

        private void ApplyFilters()
        {
            _filteredItems = FilterItems();
        }

        private void SetEmptyItem()
        {
            _itemIndex = -1;
            _itemImage = null;
        }

        private List<Product> FilterItems()
        {
            _filters = _filterConfig.GetFilters();

            FiltersModel filtersModel = new FiltersModel(_filters);
            List<IFilter> activeFilters = filtersModel.GetActiveFilters();
            List<bool> filterValues = new List<bool>();
            List<Product> filteredItems = new List<Product>();

            if (activeFilters.Count == 0)
            {
                return filteredItems = _products;
            }

            StringFilter color = null;
            ToggleFilter size = null;
            StringFilter material = null;
            BoolFilter sale = null;
            StringFilter shop = null;
            BoolFilter delivery = null;
            IntRangeFilter price = null;

            bool colorMatch = false;
            bool sizeMatch = false;
            bool materialMatch = false;
            bool saleMatch = false;
            bool shopMatch = false;
            bool deliveryMatch = false;
            bool priceMatch = false;

            foreach (Product item in _products)
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
                        shopMatch = shop.Value == item.Shop.Name;
                        filterValues.Add(shopMatch);
                    }
                    else if (filter.Name == "Возможность доставки")
                    {
                        delivery = (BoolFilter)filter;
                        deliveryMatch = delivery.Value == item.Delivery;
                        filterValues.Add(deliveryMatch);
                    }
                    else if (filter.Name == "Цена")
                    {
                        price = (IntRangeFilter)filter;
                        priceMatch = item.Price >= price.Value.x && item.Price <= price.Value.y;
                        filterValues.Add(priceMatch);
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

        private bool CheckSize(List<StringBoolModel> sizeFilter, Product item)
        {
            List<StringBoolModel> itemSize = item.Size;
            List<bool> sizeValues = new List<bool>();

            bool correctSize = false;

            foreach (StringBoolModel filterSize in sizeFilter)
            {
                foreach (StringBoolModel size in itemSize)
                {
                    correctSize = filterSize.Value == true && size.Value == true && filterSize.Text == size.Text;
                    sizeValues.Add(correctSize);
                }
            }

            bool sizeCheckResult = sizeValues.Any(v => v == true);

            return sizeCheckResult;
        }
    }
}