using System;
using System.Collections.Generic;

using SettingsActivity.Configs;

using DummyActivity.Configs;
using DummyActivity.Models;
using DummyActivity.Views;

namespace DummyActivity.Controllers
{
    public class ProductController : IDisposable
    {
        private ProductView _view;

        private DummyMainView _mainView;

        private ProductsModel _model;

        private List<Product> _products;

        private FilterConfig _filterConfig;

        public ProductController(ProductView view, DummyMainView mainView, List<Product> products, FilterConfig filterConfig)
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
            _model.InfoClick -= OnInfoClick;
            _view.NextClick -= OnViewNextClick;
            _view.PreviousClick -= OnViewPreviousClick;
            _view.InfoClick -= OnViewClick;
            _view.DescriptionPanel.OpenDescription -= OnOpenDescription;
        }

        private void Initialize()
        {
            _model = new ProductsModel(_products, _filterConfig);
            //_model.Initialize();

            if (_model.ItemImage == null)
            {
                _view.ButtonImage.gameObject.SetActive(false);
            }
            else
            {
                _view.SetButtonShape(_model.ItemImage);
                _view.EnableItemObject(_model.ItemImage);
            }

            _model.ItemChanged += OnItemChange;
            _model.InfoClick += OnInfoClick;
            _view.NextClick += OnViewNextClick;
            _view.PreviousClick += OnViewPreviousClick;
            _view.InfoClick += OnViewClick;
            _view.DescriptionPanel.OpenDescription += OnOpenDescription;
        }

        private void OnOpenDescription()
        {
            _view.DescriptionPanel.SetDescriptionText(_model.GetCurrentItem());
            _view.DescriptionPanel.SetIcon(_model.ItemImage);
            _mainView.CloseInfoPanels();
        }

        private void OnItemChange()
        {
            if (_model.ItemImage == null)
            {
                _view.ButtonImage.gameObject.SetActive(false);
            }
            else
            {
                _view.ButtonImage.gameObject.SetActive(true);
            }

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