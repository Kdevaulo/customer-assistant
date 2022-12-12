using System;
using System.Collections.Generic;
using System.Linq;

using CustomerAssistant.DummyActivity;
using CustomerAssistant.FavoritesActivity.Views;

using Cysharp.Threading.Tasks;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace CustomerAssistant.FavoritesActivity.Controllers
{
    public class ActivityController : IDisposable
    {
        private readonly BackButtonView _backBackButtonView;

        private readonly RectTransform _contentContainer;

        private readonly RectTransform _notificationCanvas;

        private readonly TextMeshProUGUI _notificationTextMeshPro;

        private readonly FavoriteImageView _favoriteContainerPrefab;

        private readonly List<Product> _favoriteProducts = new List<Product>();

        private readonly List<FavoriteImageView> _favoriteViews = new List<FavoriteImageView>();

        private readonly Dictionary<FavoriteImageView, Product> _viewProductCollection =
            new Dictionary<FavoriteImageView, Product>();

        private readonly Dictionary<Product, string> _productKeyCollection = new Dictionary<Product, string>();

        private List<Product> _usedFavorites = new List<Product>();

        private string _keyPrefs = string.Empty;

        public ActivityController(BackButtonView backBackButtonView,
            RectTransform contentContainer, FavoriteImageView favoriteContainerPrefab, RectTransform notificationCanvas,
            TextMeshProUGUI notificationTextMeshPro)
        {
            _backBackButtonView = backBackButtonView;
            _contentContainer = contentContainer;
            _favoriteContainerPrefab = favoriteContainerPrefab;
            _notificationCanvas = notificationCanvas;
            _notificationTextMeshPro = notificationTextMeshPro;

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
            _keyPrefs = PlayerPrefs.GetString(Constants.PrefsKeysContainerPattern);

            Utils.GetFavoritesFromPrefs(_favoriteProducts, (key, product) =>
            {
                _productKeyCollection.Add(product, key);
                _favoriteProducts.Add(product);

                if (_keyPrefs.Contains(key))
                {
                    _usedFavorites.Add(product);
                }
            });
        }

        private void InitializeFavorites()
        {
            ChangeContainerHeight(_favoriteProducts.Count);

            _viewProductCollection.Clear();
            _favoriteViews.Clear();

            if (_favoriteProducts.Count == 0)
            {
                Utils.CreateNotification("No favorite items", _notificationTextMeshPro, _notificationCanvas);
            }

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
                _viewProductCollection.Add(spawnedView, favoriteProduct);
            }

            foreach (var usedProduct in _usedFavorites)
            {
                var view = _viewProductCollection.First(pair => pair.Value.Image == usedProduct.Image).Key;
                view.SetOffText(); // note: change text and toggle if favorite product already uses
                view.SetToggleActive(true);
            }
        }

        private void SubscribeButtons(FavoriteImageView imageView)
        {
            imageView.RemoveButtonClicked += HandleRemoveButtonClick;
            imageView.ToggleSwitched += HandleToggleSwitch;
        }

        private void HandleRemoveButtonClick(FavoriteImageView imageView)
        {
            UnsubscribeButtons(imageView);
            HandleToggleSwitch(imageView, false);

            var productForRemoving = _viewProductCollection[imageView];
            _favoriteProducts.Remove(productForRemoving);
            _favoriteViews.Remove(imageView);

            TryRemoveFromPrefs(_productKeyCollection[productForRemoving]);

            var removableGameObject = imageView.gameObject;
            removableGameObject.SetActive(false);
            Object.Destroy(removableGameObject);

            ChangeContainerHeight(_favoriteProducts.Count);
            UpdateFavoritesPositions();
        }

        private void UnsubscribeButtons(FavoriteImageView imageView)
        {
            imageView.RemoveButtonClicked -= HandleRemoveButtonClick;
            imageView.ToggleSwitched -= HandleToggleSwitch;
        }

        private void HandleToggleSwitch(FavoriteImageView imageView, bool isActive)
        {
            var product = _viewProductCollection[imageView];

            var key = _productKeyCollection[product];

            var containsFlag = _keyPrefs.Contains(key);

            if (isActive && containsFlag || !isActive && !containsFlag)
            {
                return;
            }

            if (isActive) // note: if isActive && !containsFlag
            {
                imageView.SetOffText();

                _keyPrefs += key + Constants.PrefsKeysSeparator;

                Utils.CreateNotification("Item used", _notificationTextMeshPro, _notificationCanvas);
            }
            else
            {
                imageView.SetOnText();

                _keyPrefs = _keyPrefs.Replace(key + Constants.PrefsKeysSeparator, string.Empty);

                Utils.CreateNotification("Item not using", _notificationTextMeshPro, _notificationCanvas);
            }

            PlayerPrefs.SetString(Constants.PrefsKeysContainerPattern, _keyPrefs);
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