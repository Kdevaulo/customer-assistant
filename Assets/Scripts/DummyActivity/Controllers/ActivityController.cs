using System;

using CustomerAssistant.DummyActivity.Models;

using Cysharp.Threading.Tasks;

using DummyActivity.Views;

using Mapbox.Json;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomerAssistant.DummyActivity.Controllers
{
    public class ActivityController : IDisposable
    {
        private readonly DummyMainView _mainView;

        private readonly FavoritesModel _favoritesModel;

        public ActivityController(DummyMainView mainView, FavoritesModel favoritesModel)
        {
            _mainView = mainView;
            _favoritesModel = favoritesModel;

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
        }

        private void HandleAddToFavoriteButtonClick()
        {
            var product = _favoritesModel.GetFavoriteProduct();

            var serializedString = JsonConvert.SerializeObject(product);

            for (int i = 0; i < Constants.MaxFavorites; i++)
            {
                var prefsKey = string.Format(Constants.PrefsKeyPattern, i);

                if (PlayerPrefs.HasKey(prefsKey))
                {
                    continue;
                }

                PlayerPrefs.SetString(prefsKey, serializedString);

                Debug.Log($"todo: saved to \"{prefsKey}\"");

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