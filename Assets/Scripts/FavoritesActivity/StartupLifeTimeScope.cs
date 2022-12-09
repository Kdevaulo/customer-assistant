using CustomerAssistant.FavoritesActivity.Controllers;
using CustomerAssistant.FavoritesActivity.Views;

using UnityEngine;

namespace CustomerAssistant.FavoritesActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in FavoritesActivity")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private BackButtonView _backButtonView;

        [SerializeField] private RectTransform _contentContainer;

        [SerializeField] private FavoriteImageView _favoriteContainerPrefab;

        private void Start()
        {
            _ = new ActivityController(_backButtonView, _contentContainer, _favoriteContainerPrefab);
        }
    }
}