using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity.Models
{
    public struct ViewFilterTransformModel
    {
        private FilterView _view;

        private IFilter _typedFilter;

        private RectTransform _rectTransform;

        public ViewFilterTransformModel(FilterView view, IFilter typedFilter, RectTransform rectTransform)
        {
            _view = view;
            _typedFilter = typedFilter;
            _rectTransform = rectTransform;
        }

        public FilterView View => _view;

        public IFilter TypedFilter => _typedFilter;

        public RectTransform RectTransform => _rectTransform;
    }
}