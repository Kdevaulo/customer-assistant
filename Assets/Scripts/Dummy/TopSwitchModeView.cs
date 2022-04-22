using UnityEngine;
using UnityEngine.UI;

namespace Dummy
{
    [RequireComponent(typeof(Dropdown))]
    public class TopSwitchModeView : MonoBehaviour
    {
        [SerializeField] private GameObject _jacketView;
        [SerializeField] private GameObject _tShirtView;
        [SerializeField] private Toggle[] _descriptionToggles;
        private Dropdown _modeDropdown;
        private Dropdown.OptionData _jacket;
        private Dropdown.OptionData _tShirt;

        private void Awake()
        {
            Initialize();
            SelectMode(_modeDropdown.value);
        }

        private void OnEnable()
        {
            _modeDropdown.onValueChanged.AddListener(SelectMode);
        }

        private void OnDisable()
        {
            _modeDropdown.onValueChanged.RemoveListener(SelectMode);
        }

        private void Initialize()
        {
            _modeDropdown = GetComponent<Dropdown>();
            _jacket = new Dropdown.OptionData("Верхняя одежда");
            _tShirt = new Dropdown.OptionData("Топы");
            _modeDropdown.options.Add(_jacket);
            _modeDropdown.options.Add(_tShirt);
        }

        private void SelectMode(int mode)
        {
            mode = _modeDropdown.value;
            switch (mode)
            {
                case 0:
                    _jacketView.SetActive(true);
                    _tShirtView.SetActive(false);
                    break;
                case 1:
                    _jacketView.SetActive(false);
                    _tShirtView.SetActive(true);
                    break;
            }
            DescriptionsClose();
        }

        private void DescriptionsClose()
        {
            foreach (Toggle toggle in _descriptionToggles)
            {
                toggle.isOn = false;
            }
        }
    }
}