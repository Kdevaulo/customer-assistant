using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(StringFilterView) + " in SettingsActivity")]
    public class StringFilterView : FilterView
    {
        public InputField InputField => _inputField;

        [SerializeField] private InputField _inputField;
    }
}