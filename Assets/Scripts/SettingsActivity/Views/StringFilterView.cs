using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(StringFilterView) + " in SettingsActivity")]
    public class StringFilterView : FilterView
    {
        [SerializeField] private InputField _inputField;

        public InputField InputField => _inputField;
    }
}