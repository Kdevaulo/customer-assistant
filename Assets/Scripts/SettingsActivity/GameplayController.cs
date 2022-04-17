using System;

using SettingsActivity.Configs;
using SettingsActivity.Filters;
using SettingsActivity.Models;
using SettingsActivity.Views;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using Object = UnityEngine.Object;

namespace SettingsActivity
{
    public class GameplayController : IDisposable
    {
        private readonly BackButtonView _backButtonView;
        private readonly FiltersContainerView _filtersContainerView;
        private readonly FilterConfig _filterConfig;

        private FiltersModel _filtersModel;
        private RectTransform _containerTransform;

        public GameplayController(BackButtonView backButtonView, FiltersContainerView filtersContainerView,
            FilterConfig filterConfig)
        {
            _backButtonView = backButtonView;
            _filtersContainerView = filtersContainerView;
            _filterConfig = filterConfig;

            Start();
        }

        void IDisposable.Dispose()
        {
            _backButtonView.OnBackButtonClicked -= BackButtonClickHandler;
            _filtersContainerView.Dropdown.onValueChanged.RemoveListener(DropdownValueChangeHandler);
        }

        private void Start()
        {
            #region filters settings

            _filtersModel = new FiltersModel(_filterConfig.GetFilters());

            _filtersContainerView.Dropdown.options.Clear();

            var inactiveFilters = _filtersModel.GetInactiveFilters();

            foreach (var item in inactiveFilters)
            {
                _filtersContainerView.Dropdown.options.Add(new Dropdown.OptionData(item.Name));
            }

            var filters = _filtersModel.GetFilters();

            _containerTransform = _filtersContainerView.FiltersContainer.GetComponent<RectTransform>();

            Assert.IsNotNull(_containerTransform, "FiltersContainerView -> FiltersContainer does not contain RectTransform");

            foreach (var item in filters)
            {
                RectTransform viewTransform = null;

                switch (item.GetType().Name)
                {
                    case "BoolFilter":
                        var boolFilter = (item as BoolFilter);

                        Assert.IsNotNull(boolFilter);

                        var boolFilterView = Object.Instantiate(boolFilter.BoolFilterPrefab, _filtersContainerView.FiltersContainer);

                        boolFilterView.ToggleText.text = boolFilter.Name;

                        boolFilterView.Toggle.SetIsOnWithoutNotify(boolFilter.Value);

                        viewTransform = boolFilterView.GetComponent<RectTransform>();

                        if (!item.Active)
                        {
                            boolFilterView.gameObject.SetActive(false);
                        }

                        break;

                    case "StringFilter":
                        var stringFilter = (item as StringFilter);

                        Assert.IsNotNull(stringFilter);

                        var stringFilterView = Object.Instantiate(stringFilter.StringFilterPrefab, _filtersContainerView.FiltersContainer);

                        stringFilterView.TextField.text = stringFilter.Name;

                        stringFilterView.InputField.text = stringFilter.Value;

                        viewTransform = stringFilterView.GetComponent<RectTransform>();

                        if (!item.Active)
                        {
                            stringFilterView.gameObject.SetActive(false);
                        }

                        break;

                    case "ToggleFilter":

                        break;
                }

                if (item.Active)
                {
                    Assert.IsNotNull(viewTransform, "viewTransform == null");

                    viewTransform.anchoredPosition -= new Vector2(0, _containerTransform.rect.height);

                    ChangeContainerHeight(viewTransform.sizeDelta.y, true);
                }
            }

            #endregion

            _backButtonView.OnBackButtonClicked += BackButtonClickHandler;
            _filtersContainerView.Dropdown.onValueChanged.AddListener(DropdownValueChangeHandler);
        }

        private void ChangeContainerHeight(float height, bool increase)
        {
            var y = increase ? height : -height;

            _containerTransform.sizeDelta += new Vector2(0, y);
        }

        private void BackButtonClickHandler()
        {
            // todo: to dummy's activity
            Debug.Log("BackButton pressed");
        }

        private void DropdownValueChangeHandler(int value)
        {
            var chosenFilter = _filtersContainerView.Dropdown.options[value];

            _filtersContainerView.Dropdown.options.Remove(chosenFilter);

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