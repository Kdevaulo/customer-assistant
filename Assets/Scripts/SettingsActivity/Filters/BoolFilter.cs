using System;

using CustomerAssistant.SettingsActivity.Views;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Filters
{
    [Serializable]
    public class BoolFilter : ITypedFilter<bool>
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _active;

        [SerializeField] private bool _value;

        [SerializeField] private BoolFilterView _boolFilterPrefab;

        void IFilter.ChangeActiveState(bool state)
        {
            _active = state;
        }

        void ITypedFilter<bool>.ChangeValue(bool value)
        {
            _value = value;
        }

        public bool Active => _active;

        public string Name => _name;

        public bool Value => _value;

        public BoolFilterView BoolFilterPrefab => _boolFilterPrefab;
    }
}