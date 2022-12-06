using System;

using CustomerAssistant.SettingsActivity.Views;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Filters
{
    [Serializable]
    public class StringFilter : ITypedFilter<string>
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _active;

        [SerializeField] private string _value;

        [SerializeField] private StringFilterView _stringFilterPrefab;

        void IFilter.ChangeActiveState(bool state)
        {
            _active = state;
        }

        void ITypedFilter<string>.ChangeValue(string value)
        {
            _value = value;
        }

        public bool Active => _active;

        public string Name => _name;

        public string Value => _value;

        public StringFilterView StringFilterPrefab => _stringFilterPrefab;
    }
}