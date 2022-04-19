using System;
using System.Collections.Generic;
using System.Linq;

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
    public class ActivityController : IDisposable
    {
        private readonly ButtonView _backButtonView;
        private readonly FiltersContainerView _filtersContainerView;
        private readonly FilterConfig _filterConfig;

        private FiltersModel _filtersModel;
        private RectTransform _containerTransform;

        private List<ViewFilterTransformModel> _filtersData = new List<ViewFilterTransformModel>(8);

        public ActivityController(ButtonView backButtonView, FiltersContainerView filtersContainerView,
            FilterConfig filterConfig)
        {
            _backButtonView = backButtonView;
            _filtersContainerView = filtersContainerView;
            _filterConfig = filterConfig;

            Initialize();
        }

        void IDisposable.Dispose()
        {
            _backButtonView.ButtonClicked -= BackButtonClickHandler;
            _filtersContainerView.Dropdown.onValueChanged.RemoveListener(AddFilterHandle);
        }

        private void Initialize()
        {
            #region filters settings

            _filtersModel = new FiltersModel(_filterConfig.GetFilters());

            _filtersContainerView.Dropdown.options.Clear();

            var inactiveFilters = _filtersModel.GetInactiveFilters();

            foreach (var item in inactiveFilters)
            {
                AddDropOption(item.Name);
            }

            var filters = _filtersModel.GetFilters();

            _containerTransform = _filtersContainerView.FiltersContainer.GetComponent<RectTransform>();

            Assert.IsNotNull(_containerTransform, "FiltersContainerView -> FiltersContainer does not contain RectTransform");

            foreach (var item in filters)
            {
                FilterView filterView = null;

                switch (item.GetType().Name)
                {
                    case "BoolFilter":
                        var boolFilter = (item as BoolFilter);

                        Assert.IsNotNull(boolFilter);

                        var boolFilterView = Object.Instantiate(boolFilter.BoolFilterPrefab, _filtersContainerView.FiltersContainer);

                        SetText(boolFilterView, boolFilter.Name);
                        filterView = boolFilterView;

                        boolFilterView.Toggle.SetIsOnWithoutNotify(boolFilter.Value);

                        break;

                    case "StringFilter":
                        var stringFilter = (item as StringFilter);

                        Assert.IsNotNull(stringFilter);

                        var stringFilterView = Object.Instantiate(stringFilter.StringFilterPrefab, _filtersContainerView.FiltersContainer);

                        SetText(stringFilterView, stringFilter.Name);
                        filterView = stringFilterView;

                        stringFilterView.InputField.text = stringFilter.Value;

                        break;

                    case "ToggleFilter":

                        break;
                }

                Assert.IsNotNull(filterView, "filterView == null");

                filterView.RemoveButton.ButtonClicked += () => RemoveFilterHandle(filterView);

                var viewTransform = filterView.GetComponent<RectTransform>();

                Assert.IsNotNull(viewTransform, "viewTransform == null");

                _filtersData.Add(new ViewFilterTransformModel(filterView, item, viewTransform));

                if (item.Active)
                {
                    viewTransform.anchoredPosition -= new Vector2(0, _containerTransform.rect.height);

                    ChangeContainerHeight(viewTransform.sizeDelta.y, true);
                }
                else
                {
                    filterView.gameObject.SetActive(false);
                }
            }

            #endregion

            _backButtonView.ButtonClicked += BackButtonClickHandler;
            _filtersContainerView.Dropdown.onValueChanged.AddListener(AddFilterHandle);
        }

        private void AddFilterHandle(int value)
        {
            var chosenFilter = _filtersContainerView.Dropdown.options[value];

            RemoveDropOption(chosenFilter);

            var item = _filtersData.First(x => x.Filter.Name == chosenFilter.text);

            var transform = item.RectTransform;

            transform.anchoredPosition = new Vector2(0, -_containerTransform.rect.height);

            item.Filter.ChangeActiveState(true);
            item.View.gameObject.SetActive(true);

            ChangeContainerHeight(transform.sizeDelta.y, true);

            Debug.Log(chosenFilter.text);
        }

        private void RemoveFilterHandle(FilterView view)
        {
            ClearContainerHeight();

            foreach (var item in _filtersData)
            {
                if (item.View == view)
                {
                    item.Filter.ChangeActiveState(false);
                    item.View.gameObject.SetActive(false);

                    var viewName = item.Filter.Name;

                    AddDropOption(viewName);

                    continue;
                }

                if (item.Filter.Active)
                {
                    var viewHeight = item.RectTransform.sizeDelta.y;

                    ChangeContainerHeight(viewHeight, true);

                    item.RectTransform.anchoredPosition = new Vector2(0, viewHeight - _containerTransform.sizeDelta.y);
                }
            }
        }

        private void SetText(FilterView view, string text)
        {
            view.TextField.text = text;
        }

        private void BackButtonClickHandler()
        {
            // todo: to dummy's activity
            Debug.Log("BackButton pressed");
        }

        private void ChangeContainerHeight(float height, bool increase)
        {
            var y = increase ? height : -height;

            _containerTransform.sizeDelta += new Vector2(0, y);
        }

        private void ClearContainerHeight()
        {
            _containerTransform.sizeDelta = Vector2.zero;
        }

        private void AddDropOption(string name)
        {
            _filtersContainerView.Dropdown.options.Add(new Dropdown.OptionData(name));
        }

        private void RemoveDropOption(Dropdown.OptionData option)
        {
            _filtersContainerView.Dropdown.options.Remove(option);
        }
    }
}