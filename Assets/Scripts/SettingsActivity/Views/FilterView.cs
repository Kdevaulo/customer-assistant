using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(FilterView) + " in SettingsActivity")]
    public class FilterView : MonoBehaviour
    {
        [SerializeField] private Text _textField;

        [SerializeField] private ButtonView _removeButton;

        public Text TextField => _textField;

        public ButtonView RemoveButton => _removeButton;
    }
}