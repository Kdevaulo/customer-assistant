using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(StringFilterView) + " in SettingsActivity")]
    public class StringFilterView : MonoBehaviour
    {
        [SerializeField] private Text _textField;

        [SerializeField] private InputField _inputField;

        public Text TextField => _textField;

        public InputField InputField => _inputField;
    }
}