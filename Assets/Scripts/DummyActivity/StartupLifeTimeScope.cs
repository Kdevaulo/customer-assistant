using System;
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

using TMPro;

using UnityEngine;

namespace CustomerAssistant.DummyActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in Dummy")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;

        [SerializeField] private FilterConfig _filterConfig;

        [SerializeField] private ProductView _pantsView;

        [SerializeField] private ProductView _shirtsView;

        [SerializeField] private ProductView _tShirtView;

        [SerializeField] private DummyMainView _mainView;

        [SerializeField] private RectTransform _notificationCanvas;

        [SerializeField] private TextMeshProUGUI _notificationTextMeshPro;

        private ProductController _pantsController;

        private ProductController _shirtsController;

        private ProductController _tShirtsController;

        private ActivityController _activityController;

        private FavoritesModel _favoritesModel;

        private List<Product> _favorites = new List<Product>();

        private bool _errorOnLoad = false;

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

            DataLoader.ErrorOnLoad += HandleErrorOnLoad;

            await DataLoader.LoadItemsByShopIDAsync(stringBuilder.ToString());

            DataLoader.ErrorOnLoad -= HandleErrorOnLoad;

            if (!_errorOnLoad)
            {
                products.AddRange(DataLoader.GetProducts());
            }

            _favoritesModel = new FavoritesModel();

            var keyPrefs = PlayerPrefs.GetString(Constants.PrefsKeysContainerPattern);

            var usedFavorites = new List<Product>();

            Utils.GetFavoritesFromPrefs(_favorites, (key, product) =>
            {
                _favorites.Add(product);

                if (keyPrefs.Contains(key))
                {
                    usedFavorites.Add(product);
                }
            });

            var favoritePants = GetProductsOfType(Clothes.PANTS, usedFavorites);
            var favoriteTShirts = GetProductsOfType(Clothes.T_SHIRTS, usedFavorites);
            var favoriteShirts = GetProductsOfType(Clothes.SHIRTS, usedFavorites);

            if (favoritePants != null)
            {
                _mainView.ActivatePantsIndicator();
            }

            if (favoriteTShirts != null)
            {
                _mainView.ActivateTShirtsIndicator();
            }

            if (favoriteShirts != null)
            {
                _mainView.ActivateShirtsIndicator();
            }

            var pantsFlag = favoritePants != null;
            var tShirtsFlag = favoriteTShirts != null;
            var shirtsFlag = favoriteShirts != null;

            _pantsController = new ProductController(_pantsView, _mainView,
                pantsFlag ? favoritePants : GetProductsOfType(Clothes.PANTS, products), _filterConfig, _favoritesModel,
                pantsFlag);

            _tShirtsController = new ProductController(_tShirtView, _mainView,
                tShirtsFlag ? favoriteTShirts : GetProductsOfType(Clothes.T_SHIRTS, products), _filterConfig,
                _favoritesModel, tShirtsFlag);

            _shirtsController = new ProductController(_shirtsView, _mainView,
                shirtsFlag ? favoriteShirts : GetProductsOfType(Clothes.SHIRTS, products), _filterConfig,
                _favoritesModel, shirtsFlag);

            _activityController =
                new ActivityController(_mainView, _favoritesModel, _notificationCanvas, _notificationTextMeshPro,
                    _favorites);

            _loadingScreen.SetActive(false);
        }

        private void HandleErrorOnLoad()
        {
            _errorOnLoad = true;
        }

        private List<Product> GetProductsOfType(Clothes type, List<Product> products)
        {
            var filteredProducts = products.Where(product => product.Type == type).ToList();

            return filteredProducts.Count == 0 ? null : filteredProducts;
        }
    }
}