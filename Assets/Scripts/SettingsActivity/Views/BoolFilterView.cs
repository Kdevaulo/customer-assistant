using System;

using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(BoolFilterView) + " in SettingsActivity")]
    public class BoolFilterView : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        [SerializeField] private Text _toggleText;

        public Toggle Toggle => _toggle;

        public Text ToggleText => _toggleText;
    }
}