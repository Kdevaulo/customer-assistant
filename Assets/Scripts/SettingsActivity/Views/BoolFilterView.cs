using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(BoolFilterView) + " in SettingsActivity")]
    public class BoolFilterView : FilterView
    {
        public Toggle Toggle => _toggle;

        [SerializeField] private Toggle _toggle;
    }
}