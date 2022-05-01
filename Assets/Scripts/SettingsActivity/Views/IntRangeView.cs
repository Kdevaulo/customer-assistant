using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(IntRangeView) + " in SettingsActivity")]
    public class IntRangeView : FilterView
    {
        [SerializeField] private InputField _minInputField;
        [SerializeField] private InputField _maxInputField;

        public InputField MinInputField => _minInputField;
        public InputField MaxInputField => _maxInputField;
    }
}