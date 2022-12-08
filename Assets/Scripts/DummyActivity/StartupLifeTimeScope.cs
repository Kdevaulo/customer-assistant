using System.Collections.Generic;
using System.Linq;
using System.Text;

using CustomerAssistant.DatabaseLoadSystem;
using CustomerAssistant.DummyActivity.Controllers;
using CustomerAssistant.DummyActivity.Models;
using CustomerAssistant.MapKit;
using CustomerAssistant.SettingsActivity.Configs;

using Cysharp.Threading.Tasks;

using DummyActivity.Views;

using Mapbox.Json;

using UnityEngine;

namespace CustomerAssistant.DummyActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in Dummy")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;

        [SerializeField] FilterConfig _filterConfig;

        [SerializeField] ProductView _pantsView;

        [SerializeField] ProductView _shirtsView;

        [SerializeField] ProductView _tShirtView;

        [SerializeField] DummyMainView _mainView;

        private ProductController _pantsController;

        private ProductController _shirtsController;

        private ProductController _tShirtsController;

        private ActivityController _activityController;

        private FavoritesModel _favoritesModel;

        private void Start()
        {
            InitializeAsync().Forget();
        }

        private async UniTask InitializeAsync()
        {
            string json = PlayerPrefs.GetString("Shops");

            ShopJson shopJson = JsonConvert.DeserializeObject<ShopJson>(json);

            var products = new List<Product>();
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < shopJson.Shops.Count; i++)
            {
                stringBuilder.Append(shopJson.Shops[i]);

                if (i != shopJson.Shops.Count - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            await DataLoader.LoadItemsByShopIDAsync(stringBuilder.ToString());

            products.AddRange(DataLoader.GetProducts());

            _favoritesModel = new FavoritesModel();
            
            _pantsController = new ProductController(_pantsView, _mainView,
                GetProductsOfType(Clothes.PANTS, products), _filterConfig, _favoritesModel);

            _shirtsController = new ProductController(_shirtsView, _mainView,
                GetProductsOfType(Clothes.SHIRTS, products), _filterConfig, _favoritesModel);

            _tShirtsController = new ProductController(_tShirtView, _mainView,
                GetProductsOfType(Clothes.T_SHIRTS, products), _filterConfig, _favoritesModel);

            _activityController = new ActivityController(_mainView, _favoritesModel);
            
            _loadingScreen.SetActive(false);
        }

        private List<Product> GetProductsOfType(Clothes type, List<Product> products)
        {
            var filteredProducts = products.Where(product => product.Type == type).ToList();

            return filteredProducts.Count == 0 ? null : filteredProducts;
        }
    }
}