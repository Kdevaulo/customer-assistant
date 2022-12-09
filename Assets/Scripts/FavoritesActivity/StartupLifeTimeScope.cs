using CustomerAssistant.FavoritesActivity.Controllers;
using CustomerAssistant.FavoritesActivity.Views;

using TMPro;

using UnityEngine;

namespace CustomerAssistant.FavoritesActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in FavoritesActivity")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private BackButtonView _backButtonView;

        [SerializeField] private RectTransform _contentContainer;

        [SerializeField] private RectTransform _notificationCanvas;

        [SerializeField] private FavoriteImageView _favoriteContainerPrefab;

        [SerializeField] private TextMeshProUGUI _notificationPrefab;

        private void Start()
        {
            _ = new ActivityController(_backButtonView, _contentContainer, _favoriteContainerPrefab,
                _notificationCanvas, _notificationPrefab);
        }
    }
}