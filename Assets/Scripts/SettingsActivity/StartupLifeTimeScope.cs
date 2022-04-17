using SettingsActivity.Configs;
using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in SettingsActivity")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private FilterConfig _filterConfig;
        [SerializeField] private BackButtonView _backButtonView;
        [SerializeField] private FiltersContainerView _filtersContainerView;

        private void Start()
        {
            _ = new GameplayController(_backButtonView, _filtersContainerView, _filterConfig);
        }
    }
}