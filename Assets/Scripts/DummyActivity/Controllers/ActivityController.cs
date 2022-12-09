using System;
using System.Collections.Generic;
using System.Linq;

using CustomerAssistant.DummyActivity.Models;

using Cysharp.Threading.Tasks;

using DummyActivity.Views;

using Mapbox.Json;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomerAssistant.DummyActivity.Controllers
{
    public class ActivityController : IDisposable
    {
        private readonly DummyMainView _mainView;

        private readonly FavoritesModel _favoritesModel;

        private readonly RectTransform _notificationCanvas;

        private readonly TextMeshProUGUI _notificationTextMeshPro;

        private readonly List<Product> _currentFavorites = new List<Product>();

        public ActivityController(DummyMainView mainView, FavoritesModel favoritesModel,
            RectTransform notificationCanvas,
            TextMeshProUGUI notificationTextMeshPro)
        {
            _mainView = mainView;
            _favoritesModel = favoritesModel;
            _notificationCanvas = notificationCanvas;
            _notificationTextMeshPro = notificationTextMeshPro;

            Initialize();
        }

        void IDisposable.Dispose()
        {
            _mainView.AddToFavoriteButtonClicked -= HandleAddToFavoriteButtonClick;
            _mainView.MapButtonClicked -= HandleMapButtonClick;
            _mainView.SettingsButtonClicked -= HandleSettingsButtonClick;
            _mainView.FavoritesButtonClicked -= HandleFavoritesButtonClick;
        }

        private void Initialize()
        {
            _mainView.AddToFavoriteButtonClicked += HandleAddToFavoriteButtonClick;
            _mainView.MapButtonClicked += HandleMapButtonClick;
            _mainView.SettingsButtonClicked += HandleSettingsButtonClick;
            _mainView.FavoritesButtonClicked += HandleFavoritesButtonClick;

            _currentFavorites.Clear();

            // todo: remove duplicated code (Favorites ActivityController)
            for (int i = 0; i < Constants.MaxFavorites; i++)
            {
                // todo: remove key dependency on index using route table
                var key = string.Format(Constants.PrefsKeyPattern, i);

                if (!PlayerPrefs.HasKey(key))
                {
                    break;
                }

                var serializedString = PlayerPrefs.GetString(key);

                var product = JsonConvert.DeserializeObject<Product>(serializedString);

                product.Sprite = Utils.ConvertSprite(product.Image);

                _currentFavorites.Add(product);
            }

            for (var i = 0; i < _currentFavorites.Count; i++)
            {
                var item = _currentFavorites[i];
                Debug.Log($"{item} {i}");
            }
        }

        private void HandleAddToFavoriteButtonClick()
        {
            var product = _favoritesModel.GetFavoriteProduct();

            if (_currentFavorites.Any(x => x.Image == product.Image))
            {
                Utils.CreateNotification("Already in favorites", _notificationTextMeshPro, _notificationCanvas);

                return;
            }

            var serializedString = JsonConvert.SerializeObject(product);

            for (int i = 0; i < Constants.MaxFavorites; i++)
            {
                var prefsKey = string.Format(Constants.PrefsKeyPattern, i);

                if (PlayerPrefs.HasKey(prefsKey))
                {
                    continue;
                }

                PlayerPrefs.SetString(prefsKey, serializedString);

                _currentFavorites.Add(product);

                Utils.CreateNotification("Product added to favorites", _notificationTextMeshPro, _notificationCanvas);
                break;
            }
        }

        private void HandleMapButtonClick()
        {
            LoadMapSceneAsync().Forget();
        }

        private void HandleSettingsButtonClick()
        {
            LoadSettingsSceneAsync().Forget();
        }

        private void HandleFavoritesButtonClick()
        {
            LoadFavoritesSceneAsync().Forget();
        }

        private async UniTaskVoid LoadMapSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/Map");
        }

        private async UniTaskVoid LoadSettingsSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/SettingsActivity");
        }

        private async UniTaskVoid LoadFavoritesSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/FavoritesActivity");
        }
    }
}