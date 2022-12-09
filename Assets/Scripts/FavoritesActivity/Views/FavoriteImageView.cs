using System;

using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.FavoritesActivity.Views
{
    public class FavoriteImageView : MonoBehaviour
    {
        public event Action<FavoriteImageView> RemoveButtonClicked = delegate { };
        public event Action<FavoriteImageView> UseButtonClicked = delegate { };

        public Image TargetImage => _targetImage;

        public RectTransform FavoriteRectTransform => _favoriteRectTransform;
        public RectTransform ImageRectTransform => _imageRectTransform;
        public RectTransform ObjectContainerRectTransform => _objectContainerRectTransform;

        [SerializeField] private Image _targetImage;

        [SerializeField] private RectTransform _favoriteRectTransform;

        [SerializeField] private RectTransform _imageRectTransform;

        [SerializeField] private RectTransform _objectContainerRectTransform;

        [SerializeField] private Button _removeButton;

        [SerializeField] private Button _useButton;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(HandleRemoveButtonClick);
            _useButton.onClick.AddListener(HandleUseButtonClick);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(HandleRemoveButtonClick);
            _useButton.onClick.RemoveListener(HandleUseButtonClick);
        }

        private void HandleRemoveButtonClick()
        {
            RemoveButtonClicked.Invoke(this);
        }

        private void HandleUseButtonClick()
        {
            UseButtonClicked.Invoke(this);
        }
    }
}