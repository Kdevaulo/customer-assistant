using System;
using System.Collections.Generic;

using CustomerAssistant.DummyActivity;
using CustomerAssistant.FavoritesActivity.Views;

using Cysharp.Threading.Tasks;

using Mapbox.Json;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomerAssistant.FavoritesActivity.Controllers
{
    public class ActivityController : IDisposable
    {
        private readonly BackButtonView _backBackButtonView;

        private readonly RectTransform _contentContainer;

        private readonly FavoriteImageView _favoriteContainerPrefab;

        private List<Product> _favoriteProducts = new List<Product>();

        public ActivityController(BackButtonView backBackButtonView,
            RectTransform contentContainer, FavoriteImageView favoriteContainerPrefab)
        {
            _backBackButtonView = backBackButtonView;
            _contentContainer = contentContainer;
            _favoriteContainerPrefab = favoriteContainerPrefab;

            Initialize();
        }

        void IDisposable.Dispose()
        {
            _backBackButtonView.ButtonClicked -= HandleBackBackButtonClick;
        }

        private void Initialize()
        {
            _backBackButtonView.ButtonClicked += HandleBackBackButtonClick;

            LoadFavorites();
            InitializeFavorites();
        }

        private void LoadFavorites()
        {
            _favoriteProducts.Clear();

            for (int i = 0; i < Constants.MaxFavorites; i++)
            {
                var key = string.Format(Constants.PrefsKeyPattern, i);

                if (!PlayerPrefs.HasKey(key))
                {
                    break;
                }

                Debug.Log($"got prefs at \"{key}\"");

                var serializedString = PlayerPrefs.GetString(key);

                var product = JsonConvert.DeserializeObject<Product>(serializedString);

                product.Sprite = Utils.ConvertSprite(product.Image);

                _favoriteProducts.Add(product);
            }
        }

        private void InitializeFavorites()
        {
            var targetHeight = Math.Abs(_favoriteProducts.Count * Constants.FavoritesYOffset);

            var contentContainerRect = _contentContainer.rect;

            if (targetHeight > contentContainerRect.height)
            {
                _contentContainer.sizeDelta = new Vector2(_contentContainer.sizeDelta.x, targetHeight);
            }

            for (var i = 0; i < _favoriteProducts.Count; i++)
            {
                var spawnedView = UnityEngine.Object.Instantiate(_favoriteContainerPrefab, _contentContainer);

                var targetImage = spawnedView.TargetImage;
                targetImage.sprite = _favoriteProducts[i].Sprite;
                targetImage.SetNativeSize();

                var ratio = targetImage.sprite.rect.size.y / spawnedView.ObjectContainerRectTransform.rect.height;

                targetImage.rectTransform.localScale = new Vector3(1 / ratio, 1 / ratio, 1);

                spawnedView.FavoriteRectTransform.anchoredPosition =
                    new Vector2(spawnedView.ImageRectTransform.position.x, Constants.FavoritesYOffset * i);
            }
        }

        private void HandleBackBackButtonClick()
        {
            ChangeSceneAsync().Forget();
        }

        private async UniTaskVoid ChangeSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/Dummy_demo");
        }
    }
}