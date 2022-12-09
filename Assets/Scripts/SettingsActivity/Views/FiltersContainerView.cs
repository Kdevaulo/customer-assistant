using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Views
{
    [AddComponentMenu(nameof(FiltersContainerView) + " in SettingsActivity")]
    public class FiltersContainerView : MonoBehaviour
    {
        public Dropdown Dropdown => _dropdown;

        public Transform FiltersContainer => _filtersContainer;

        [SerializeField] private Dropdown _dropdown;

        [SerializeField] private Transform _filtersContainer;
    }
}