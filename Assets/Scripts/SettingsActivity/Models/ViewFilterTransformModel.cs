using CustomerAssistant.SettingsActivity.Views;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Models
{
    public struct ViewFilterTransformModel
    {
        public FilterView View => _view;

        public IFilter Filter => _filter;

        public RectTransform RectTransform => _rectTransform;

        private FilterView _view;

        private IFilter _filter;

        private RectTransform _rectTransform;

        public ViewFilterTransformModel(FilterView view, IFilter filter, RectTransform rectTransform)
        {
            _view = view;
            _filter = filter;
            _rectTransform = rectTransform;
        }
    }
}