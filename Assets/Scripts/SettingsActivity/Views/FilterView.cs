using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(FilterView) + " in SettingsActivity")]
    [RequireComponent(typeof(Collider2D))]
    public class FilterView : MonoBehaviour
    {
        [SerializeField] private Text _textField;

        [SerializeField] private ButtonView _removeButton;

        public Text TextField => _textField;

        public ButtonView RemoveButton => _removeButton;
    }
}