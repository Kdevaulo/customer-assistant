using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(FiltersContainerView) + " in SettingsActivity")]
    public class FiltersContainerView : MonoBehaviour
    {
        [SerializeField] private Dropdown _dropdown;

        [SerializeField] private Transform _filtersContainer;

        public Dropdown Dropdown => _dropdown;

        public Transform FiltersContainer => _filtersContainer;
    }
}