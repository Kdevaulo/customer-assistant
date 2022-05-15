using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using DummyActivity.Configs;
using DummyActivity.Models;
using DummyActivity.Views;

using SettingsActivity.Configs;

using UnityEngine.SceneManagement;

namespace DummyActivity.Controllers
{
    public class ProductController : IDisposable
    {
        private ProductView _view;

        private DummyMainView _mainView;

        private ProductsModel _model;

        private List<Product> _products;

        private FilterConfig _filterConfig;

        public ProductController(ProductView view, DummyMainView mainView, List<Product> products,
            FilterConfig filterConfig)
        {
            _view = view;
            _mainView = mainView;
            _products = products;
            _filterConfig = filterConfig;

            Initialize();
        }

        void IDisposable.Dispose()
        {
            _model.ItemChanged -= OnItemChange;
            _model.ItemSelected -= OnInfoClick;

            _view.NextClick -= OnViewNextClick;
            _view.PreviousClick -= OnViewPreviousClick;
            _view.InfoClick -= OnViewClick;
            _view.DescriptionPanel.OpenDescription -= OnOpenDescription;

            _mainView.MapButtonClicked -= HandleMapButtonClick;
            _mainView.SettingsButtonClicked -= HandleSettingsButtonClick;
        }

        private void Initialize()
        {
            _model = new ProductsModel(_products, _filterConfig);

            if (_model.ItemImage == null)
            {
                _view.ButtonImage.gameObject.SetActive(false);

                Product.ClothesType clothesType = _products[0].Type;

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

            _mainView.MapButtonClicked += HandleMapButtonClick;
            _mainView.SettingsButtonClicked += HandleSettingsButtonClick;
        }

        private void HandleMapButtonClick()
        {
            LoadMapSceneAsync().Forget();
        }

        private void HandleSettingsButtonClick()
        {
            LoadSettingsSceneAsync().Forget();
        }

        private async UniTaskVoid LoadMapSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/Map");
        }

        private async UniTaskVoid LoadSettingsSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/SettingsActivity");
        }

        private void FiltersNotMatch(Product.ClothesType clothesType)
        {
            switch (clothesType)
            {
                case Product.ClothesType.PANTS:
                    _mainView.ShowMessage($"Товары в категории брюки не найдены\n");
                    break;
                case Product.ClothesType.SHIRTS:
                    _mainView.ShowMessage($"Товары в категории верх не найдены\n");
                    break;
                case Product.ClothesType.T_SHIRTS:
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
        }

        private void OnViewClick(bool clicked)
        {
            _model.ShowInfo(clicked);
            OnInfoClick(clicked);
        }
    }
}