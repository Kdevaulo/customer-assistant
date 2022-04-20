using SettingsActivity.Models;

using UnityEngine;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(ToggleFilterView) + " in SettingsActivity")]
    public class ToggleFilterView : FilterView
    {
        [SerializeField] private ToggleTextModel[] _toggleModels;

        public ToggleTextModel[] ToggleModels => _toggleModels;
    }
}