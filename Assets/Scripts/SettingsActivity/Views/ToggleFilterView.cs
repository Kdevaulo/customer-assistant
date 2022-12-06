using CustomerAssistant.SettingsActivity.Models;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(ToggleFilterView) + " in SettingsActivity")]
    public class ToggleFilterView : FilterView
    {
        [SerializeField] private ToggleTextModel[] _toggleModels;

        public ToggleTextModel[] ToggleModels => _toggleModels;
    }
}