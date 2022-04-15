using System;
using System.Collections.Generic;
using System.Linq;

using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity
{
    public class GameplayController
    {
        private readonly BackButtonView _backButtonView;
        private readonly DropdownView _dropdownView;
        private List<string> _filters = new List<string>();

        public GameplayController(BackButtonView backButtonView, DropdownView dropdownView)
        {
            _backButtonView = backButtonView;
            _dropdownView = dropdownView;

            Start();
        }

        private void Start()
        {
            _filters.AddRange(_dropdownView.Dropdown.options.Select(x => x.text));

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

            SwitchFilterOn(chosenFilter.text);

            Debug.Log(chosenFilter.text);
        }

        private void SwitchFilterOn(string filterName)
        {
        }

        private void SwitchFilterOff(string filterName)
        {
        }
    }
}