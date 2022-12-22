using System;
using System.Collections.Generic;

using CustomerAssistant.DummyActivity.Models;
using CustomerAssistant.SettingsActivity.Configs;

using DummyActivity.Views;

using UnityEngine;

namespace CustomerAssistant.DummyActivity.Controllers
{
    public class ProductController : IDisposable
    {
        private readonly FavoritesModel _favoritesModel;

        private ProductView _view;

        private DummyMainView _mainView;

        private ProductsModel _model;

        private List<Product> _products;

        private FilterConfig _filterConfig;

        private bool _usingFavoriteProducts;

        public ProductController(ProductView view, DummyMainView mainView, List<Product> products,
            FilterConfig filterConfig, FavoritesModel favoritesModel, bool usingFavoriteProduct)
        {
            if (products == null)
                return;

            _view = view;
            _mainView = mainView;
            _products = products;
            _filterConfig = filterConfig;
            _favoritesModel = favoritesModel;
            _usingFavoriteProducts = usingFavoriteProduct;

            GenerateItems();
        }

        void IDisposable.Dispose()
        {
            _model.ItemChanged -= OnItemChange;
            _model.ItemSelected -= OnInfoClick;

            _view.NextClick -= OnViewNextClick;
            _view.PreviousClick -= OnViewPreviousClick;
            _view.InfoClick -= OnViewClick;
            _view.DescriptionPanel.OpenDescription -= OnOpenDescription;
        }

        private void GenerateItems()
        {
            var parent = _view.ObjectsParent;

            var sortingOrder = _view.RendererSortingOrder;

            var renderersCollection = new List<SpriteRenderer>();

            foreach (var product in _products) // todo: heavy operation, need to rework and distribute
            {
                var gameObject = new GameObject(product.Name);
                gameObject.transform.SetParent(parent);
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localScale = Vector3.one;
                gameObject.SetActive(false);

                var renderer = gameObject.AddComponent<SpriteRenderer>();
                renderer.sprite = product.Sprite;
                renderer.color = Color.white;
                renderer.sortingOrder = sortingOrder;

                renderersCollection.Add(renderer);
            }

            _view.SetItemObjects(renderersCollection.ToArray());

            Initialize();
        }

        private void Initialize()
        {
            _model = new ProductsModel(_products, _filterConfig, _usingFavoriteProducts);

            if (_model.ItemImage == null && !_usingFavoriteProducts)
            {
                Clothes clothesType = _products[0].Type;
                _view.ButtonImage.gameObject.SetActive(false);

                FiltersNotMatch(clothesType);
            }
            else
            {
                _view.SetButtonShape(_model.ItemImage);
                _view.EnableItemObject(_model.ItemImage);
            }

            _model.ItemChanged += OnItemChange;
            _model.ItemSelected += OnInfoClick;

            _view.NextClick += OnViewNextClick;
            _view.PreviousClick += OnViewPreviousClick;
            _view.InfoClick += OnViewClick;
            _view.DescriptionPanel.OpenDescription += OnOpenDescription;
        }

        private void FiltersNotMatch(Clothes clothesType)
        {
            switch (clothesType)
            {
                case Clothes.PANTS:
                    _mainView.ShowMessage($"Товары в категории брюки не найдены\n");
                    break;
                case Clothes.SHIRTS:
                    _mainView.ShowMessage($"Товары в категории верх не найдены\n");
                    break;
                case Clothes.T_SHIRTS:
                    _mainView.ShowMessage($"Товары в категории топы не найдены\n");
                    break;
            }
        }

        private void OnOpenDescription()
        {
            _view.DescriptionPanel.SetDescriptionText(_model.GetCurrentItem());
            _view.DescriptionPanel.SetIcon(_model.ItemImage);

            _mainView.CloseInfoPanels();
        }

        private void OnItemChange()
        {
            bool hasImage = _model.ItemImage != null ? true : false;

            _view.ButtonImage.gameObject.SetActive(hasImage);

            _view.SetButtonShape(_model.ItemImage);
            _view.EnableItemObject(_model.ItemImage);
            _mainView.CloseInfoPanels();
        }

        private void OnViewNextClick()
        {
            _model.NextItem();
            OnItemChange();
        }

        private void OnViewPreviousClick()
        {
            _model.PreviousItem();
            OnItemChange();
        }

        private void OnInfoClick(bool clicked)
        {
            Product selectedItem = _model.GetCurrentItem();
            if (selectedItem == null)
                return;

            _view.SetDescriptionText(selectedItem);

            _view.DescriptionPanel.gameObject.SetActive(clicked);

            if (!clicked)
            {
                _favoritesModel.CacheFavoriteProduct(selectedItem);
            }
        }

        private void OnViewClick(bool clicked)
        {
            _model.ShowInfo(clicked);
        }
    }
}