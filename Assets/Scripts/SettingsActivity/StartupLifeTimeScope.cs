using SettingsActivity.Configs;
using SettingsActivity.Controllers;
using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in SettingsActivity")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private FilterConfig _filterConfig;
        
        [SerializeField] private ButtonView _buttonView;
        
        [SerializeField] private FiltersContainerView _filtersContainerView;

        private void Start()
        {
            _ = new ActivityController(_buttonView, _filtersContainerView, _filterConfig);
        }
    }
}