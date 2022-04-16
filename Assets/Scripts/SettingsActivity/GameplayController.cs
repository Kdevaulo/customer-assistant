using System.Linq;

using SettingsActivity.Configs;
using SettingsActivity.Models;
using SettingsActivity.Views;

using UnityEngine;
using UnityEngine.UI;

namespace SettingsActivity
{
    public class GameplayController
    {
        private readonly BackButtonView _backButtonView;
        private readonly DropdownView _dropdownView;
        private readonly FilterConfig _filterConfig;

        private FiltersModel _filtersModel;

        public GameplayController(BackButtonView backButtonView, DropdownView dropdownView,
            FilterConfig filterConfig)
        {
            _backButtonView = backButtonView;
            _dropdownView = dropdownView;
            _filterConfig = filterConfig;

            Start();
        }

        private void Start()
        {
            #region filtersModels settings

            var config = _filterConfig;

            _filtersModel = new FiltersModel(config.GetColor(), config.GetSize(), config.GetMaterial(),
                config.GetSale(), config.GetShop(), config.GetDelivery());

            #endregion

            #region dropdown settings

            _dropdownView.Dropdown.options.Clear();

            var inactiveFilters = _filtersModel.GetInactiveFilters();

            foreach (var item in inactiveFilters)
            {
                _dropdownView.Dropdown.options.Add(new Dropdown.OptionData(item.Name));
            }

            #endregion

            _backButtonView.OnBackButtonClicked += BackButtonClickHandler;
            _dropdownView.Dropdown.onValueChanged.AddListener(DropdownValueChangeHandler);
        }

        private void BackButtonClickHandler()
        {
            // todo: to dummy's activity
            Debug.Log("BackButton pressed");
        }

        private void DropdownValueChangeHandler(int value)
        {
            var chosenFilter = _dropdownView.Dropdown.options[value];

            _dropdownView.Dropdown.options.Remove(chosenFilter);

            EnableFilter(chosenFilter.text);

            Debug.Log(chosenFilter.text);
        }

        private void EnableFilter(string filterName)
        {
            Debug.Log(_filtersModel.EnableFilter(filterName));
        }

        private void DisableFilter(string filterName)
        {
            Debug.Log(_filtersModel.DisableFilter(filterName));
        }
    }
}