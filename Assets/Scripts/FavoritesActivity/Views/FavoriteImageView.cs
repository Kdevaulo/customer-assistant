using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.FavoritesActivity.Views
{
    public class FavoriteImageView : MonoBehaviour
    {
        public Image TargetImage => _targetImage;

        public RectTransform FavoriteRectTransform => _favoriteRectTransform;
        public RectTransform ImageRectTransform => _imageRectTransform;

        public RectTransform ObjectContainerRectTransform => _objectContainerRectTransform;

        public Button RemoveButton => _removeButton;

        public Button UseButton => _useButton;

        [SerializeField] private Image _targetImage;

        [SerializeField] private RectTransform _favoriteRectTransform;

        [SerializeField] private RectTransform _imageRectTransform;

        [SerializeField] private RectTransform _objectContainerRectTransform;

        [SerializeField] private Button _removeButton;

        [SerializeField] private Button _useButton;
    }
}