using UnityEngine;

using DummyActivity.Configs;
using DummyActivity.Views;
using DummyActivity.Controllers;

using SettingsActivity.Configs;

namespace DummyActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in Dummy")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] ProductsConfig _productsConfig;

        [SerializeField] FilterConfig _filterConfig;

        [SerializeField] ProductView _pantsView;

        [SerializeField] ProductView _shirtsView;

        [SerializeField] ProductView _tShirtView;

        [SerializeField] DummyMainView _mainView;

        private ProductController _pantsController;

        private ProductController _shirtsController;

        private ProductController _tShirtsController;

        private void Start()
        {
            _pantsController = new ProductController(_pantsView, _mainView, _productsConfig.GetProductsOfType(Product.ClothesType.PANTS), _filterConfig);

            _shirtsController = new ProductController(_shirtsView, _mainView, _productsConfig.GetProductsOfType(Product.ClothesType.SHIRTS), _filterConfig);

            _tShirtsController = new ProductController(_tShirtView, _mainView, _productsConfig.GetProductsOfType(Product.ClothesType.T_SHIRTS), _filterConfig);
        }
    }
}