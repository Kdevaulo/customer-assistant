using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(BoolFilterView) + " in SettingsActivity")]
    public class BoolFilterView : FilterView
    {
        [SerializeField] private Toggle _toggle;

        public Toggle Toggle => _toggle;
    }
}