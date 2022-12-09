using System;
using System.Collections.Generic;

using CustomerAssistant.DummyActivity;
using CustomerAssistant.FavoritesActivity.Views;

using Cysharp.Threading.Tasks;

using Mapbox.Json;

using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace CustomerAssistant.FavoritesActivity.Controllers
{
    public class ActivityController : IDisposable
    {
        private readonly BackButtonView _backBackButtonView;

        private readonly RectTransform _contentContainer;

        private readonly FavoriteImageView _favoriteContainerPrefab;

        private readonly List<Product> _favoriteProducts = new List<Product>();

        private readonly List<FavoriteImageView> _favoriteViews = new List<FavoriteImageView>();

        private Dictionary<FavoriteImageView, Product> _productViewCollection =
            new Dictionary<FavoriteImageView, Product>();

        private Dictionary<Product, string> _productKeyCollection = new Dictionary<Product, string>();

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
            UpdateFavoritesPositions();
        }

        private void LoadFavorites()
        {
            _favoriteProducts.Clear();

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

                _productKeyCollection.Add(product, key);
                _favoriteProducts.Add(product);
            }
        }

        private void InitializeFavorites()
        {
            ChangeContainerHeight(_favoriteProducts.Count);

            _productViewCollection.Clear();
            _favoriteViews.Clear();

            foreach (var favoriteProduct in _favoriteProducts)
            {
                var spawnedView = Object.Instantiate(_favoriteContainerPrefab, _contentContainer);

                SubscribeButtons(spawnedView);

                var targetImage = spawnedView.TargetImage;
                targetImage.sprite = favoriteProduct.Sprite;
                targetImage.SetNativeSize();

                var ratio = targetImage.sprite.rect.size.y / spawnedView.ObjectContainerRectTransform.rect.height;

                targetImage.rectTransform.localScale = new Vector3(1 / ratio, 1 / ratio, 1);

                _favoriteViews.Add(spawnedView);
                _productViewCollection.Add(spawnedView, favoriteProduct);
            }
        }

        private void HandleRemoveButtonClick(FavoriteImageView imageView)
        {
            UnsubscribeButtons(imageView);

            var productForRemoving = _productViewCollection[imageView];
            _favoriteProducts.Remove(productForRemoving);
            _favoriteViews.Remove(imageView);

            TryRemoveFromPrefs(_productKeyCollection[productForRemoving]);

            var removableGameObject = imageView.gameObject;
            removableGameObject.SetActive(false);
            Object.Destroy(removableGameObject);

            ChangeContainerHeight(_favoriteProducts.Count);
            UpdateFavoritesPositions();
        }

        private void HandleUseButtonClick(FavoriteImageView imageView)
        {
            // todo: add to playerPrefs
        }

        private void ChangeContainerHeight(int productsCount)
        {
            var targetHeight = Math.Abs(productsCount * Constants.FavoritesYOffset);

            if (targetHeight > _contentContainer.rect.height)
            {
                _contentContainer.sizeDelta = new Vector2(_contentContainer.sizeDelta.x, targetHeight);
            }
        }

        private void UpdateFavoritesPositions()
        {
            for (var i = 0; i < _favoriteViews.Count; i++)
            {
                var view = _favoriteViews[i];
                view.FavoriteRectTransform.anchoredPosition = new Vector2(view.ImageRectTransform.position.x,
                    Constants.FavoritesYOffset * i);
            }
        }

        private void TryRemoveFromPrefs(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
            }
        }

        private void SubscribeButtons(FavoriteImageView imageView)
        {
            imageView.RemoveButtonClicked += HandleRemoveButtonClick;
            imageView.UseButtonClicked += HandleUseButtonClick;
        }

        private void UnsubscribeButtons(FavoriteImageView imageView)
        {
            imageView.RemoveButtonClicked -= HandleRemoveButtonClick;
            imageView.UseButtonClicked -= HandleUseButtonClick;
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