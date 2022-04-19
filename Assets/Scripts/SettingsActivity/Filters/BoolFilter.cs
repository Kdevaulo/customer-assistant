using System;

using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity.Filters
{
    [Serializable]
    public class BoolFilter : ITypedFilter<bool>
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _active;

        [SerializeField] private bool _value;

        [SerializeField] private BoolFilterView _boolFilterPrefab;

        public void ChangeActiveState(bool state)
        {
            _active = state;
        }

        public void ChangeValue(bool value)
        {
            _value = value;
        }

        public bool Active => _active;

        public string Name => _name;

        public bool Value => _value;

        public BoolFilterView BoolFilterPrefab => _boolFilterPrefab;
    }
}