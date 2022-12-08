using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(FilterView) + " in SettingsActivity")]
    public class FilterView : MonoBehaviour
    {
        [SerializeField] private Text _textField;

        [SerializeField] private BackButtonView _removeBackButton;

        public Text TextField => _textField;

        public BackButtonView RemoveBackButton => _removeBackButton;
    }
}