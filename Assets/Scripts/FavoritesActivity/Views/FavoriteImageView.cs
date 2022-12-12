using System;

using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.FavoritesActivity.Views
{
    public class FavoriteImageView : MonoBehaviour
    {
        public event Action<FavoriteImageView> RemoveButtonClicked = delegate { };
        public event Action<FavoriteImageView, bool> ToggleSwitched = delegate { };

        public Image TargetImage => _targetImage;

        public RectTransform FavoriteRectTransform => _favoriteRectTransform;
        public RectTransform ImageRectTransform => _imageRectTransform;
        public RectTransform ObjectContainerRectTransform => _objectContainerRectTransform;

        [SerializeField] private Image _targetImage;

        [SerializeField] private RectTransform _favoriteRectTransform;

        [SerializeField] private RectTransform _imageRectTransform;

        [SerializeField] private RectTransform _objectContainerRectTransform;

        [SerializeField] private Button _removeButton;

        [SerializeField] private Toggle _switchToggle;

        [SerializeField] private Text _toggleText;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(HandleRemoveButtonClick);
            _switchToggle.onValueChanged.AddListener(HandleToggleSwitch);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(HandleRemoveButtonClick);
            _switchToggle.onValueChanged.RemoveListener(HandleToggleSwitch);
        }

        public void SetOnText()
        {
            _toggleText.text = "Switch On";
        }

        public void SetOffText()
        {
            _toggleText.text = "Switch Off";
        }

        public void SetToggleActive(bool value)
        {
            _switchToggle.SetIsOnWithoutNotify(value);
        }
        
        private void HandleRemoveButtonClick()
        {
            RemoveButtonClicked.Invoke(this);
        }

        private void HandleToggleSwitch(bool isActive)
        {
            ToggleSwitched.Invoke(this, isActive);
        }
    }
}