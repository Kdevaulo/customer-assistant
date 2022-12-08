using CustomerAssistant.SettingsActivity.Configs;
using CustomerAssistant.SettingsActivity.Controllers;
using CustomerAssistant.SettingsActivity.Views;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in SettingsActivity")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private FilterConfig _filterConfig;
        
        [SerializeField] private BackButtonView _backButtonView;
        
        [SerializeField] private FiltersContainerView _filtersContainerView;

        private void Start()
        {
            _ = new ActivityController(_backButtonView, _filtersContainerView, _filterConfig);
        }
    }
}