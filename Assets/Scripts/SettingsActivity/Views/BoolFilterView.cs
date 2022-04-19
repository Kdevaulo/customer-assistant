using System;

using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(BoolFilterView) + " in SettingsActivity")]
    public class BoolFilterView : FilterView
    {
        [SerializeField] private Toggle _toggle;
        
        public Toggle Toggle => _toggle;
    }
}