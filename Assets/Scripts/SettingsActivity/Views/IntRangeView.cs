using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(IntRangeView) + " in SettingsActivity")]
    public class IntRangeView : FilterView
    {
        [SerializeField] private InputField _minInputField;

        [SerializeField] private InputField _maxInputField;

        public InputField MinInputField => _minInputField;

        public InputField MaxInputField => _maxInputField;

        public void ValidateInput()
        {
            if (_minInputField.text != string.Empty && int.Parse(_minInputField.text) < 0)
            {
                _minInputField.text = "0";
            }

            if (_maxInputField.text != string.Empty && int.Parse(_maxInputField.text) < int.Parse(_minInputField.text))
            {
                _maxInputField.text = _minInputField.text;
            }
        }
    }
}