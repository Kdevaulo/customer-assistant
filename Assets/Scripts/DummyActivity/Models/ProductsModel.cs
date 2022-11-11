using System;
using System.Collections.Generic;
using System.Linq;

using SettingsActivity;
using SettingsActivity.Configs;
using SettingsActivity.Filters;
using SettingsActivity.Models;

using UnityEngine;

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
        public event Action<bool> ItemSelected;

        public Sprite ItemImage => _itemImage;

        public void NextItem()
        {
            if (_filteredItems == null)
                return;

            if (_itemIndex < _filteredItems.Count - 1)
            {
                _itemIndex += 1;
                _itemImage = _filteredItems[_itemIndex].Sprite;
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
                _itemImage = _filteredItems[_itemIndex].Sprite;
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

            ItemSelected?.Invoke(clicked);
        }

        private void Initialize()
        {
            ApplyFilters();

            if (_filteredItems.Count == 0)
            {
                SetEmptyItem();
                return;
            }

            _itemIndex = 0;
            _itemImage = _filteredItems[_itemIndex].Sprite;
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

            StringFilter color;
            ToggleFilter size;
            StringFilter material;
            BoolFilter sale;
            StringFilter shop;
            BoolFilter delivery;
            IntRangeFilter price;

            bool colorMatch;
            bool sizeMatch;
            bool materialMatch;
            bool saleMatch;
            bool shopMatch;
            bool deliveryMatch;
            bool priceMatch;

            foreach (Product item in _products)
            {
                foreach (IFilter filter in activeFilters)
                {
                    switch (filter.Name)
                    {
                        case "Цвет":
                            color = (StringFilter) filter;
                            colorMatch = color.Value == item.Color;
                            filterValues.Add(colorMatch);
                            break;
                        case "Размер":
                            size = (ToggleFilter) filter;
                            sizeMatch = CheckSize(size.Value, item);
                            filterValues.Add(sizeMatch);
                            break;
                        case "Материал":
                            material = (StringFilter) filter;
                            materialMatch = material.Value == item.Material;
                            filterValues.Add(materialMatch);
                            break;
                        case "Наличие скидки":
                            sale = (BoolFilter) filter;
                            saleMatch = sale.Value == item.Sale;
                            filterValues.Add(saleMatch);
                            break;
                        case "Магазин":
                            shop = (StringFilter) filter;
                            shopMatch = shop.Value == item.Shop_Name;
                            filterValues.Add(shopMatch);
                            break;
                        case "Возможность доставки":
                            delivery = (BoolFilter) filter;
                            deliveryMatch = delivery.Value == item.Delivery;
                            filterValues.Add(deliveryMatch);
                            break;
                        case "Цена":
                            price = (IntRangeFilter) filter;
                            priceMatch = item.Price >= price.Value.x && item.Price <= price.Value.y;
                            filterValues.Add(priceMatch);
                            break;
                    }
                }

                if (!filteredItems.Contains(item) && filterValues.All(v => v))
                {
                    filteredItems.Add(item);
                }

                filterValues.Clear();
            }

            return filteredItems;
        }

        private bool CheckSize(List<StringBoolModel> sizeFilter, Product item)
        {
            var itemSizes = new List<StringBoolModel>();

            var sizes = item.Size.Split(',');

            foreach (var size in sizes)
            {
                itemSizes.Add(new StringBoolModel(size, true));
            }

            List<bool> sizeValues = new List<bool>();

            bool correctSize = false;

            foreach (StringBoolModel filterSize in sizeFilter)
            {
                foreach (StringBoolModel size in itemSizes)
                {
                    correctSize = filterSize.Value == true && size.Value == true && filterSize.Text == size.Text;
                    sizeValues.Add(correctSize);
                }
            }

            bool sizeCheckResult = sizeValues.Any(v => v);

            return sizeCheckResult;
        }
    }
}