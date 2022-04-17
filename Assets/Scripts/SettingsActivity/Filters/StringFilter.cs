using System;

using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity.Filters
{
    [Serializable]
    public class StringFilter : IFilter
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _active;

        [SerializeField] private string _value;

        [SerializeField] private StringFilterView _stringFilterPrefab;

        public bool Active => _active;

        public string Name => _name;

        public string Value => _value;

        public StringFilterView StringFilterPrefab => _stringFilterPrefab;

        public void ChangeActiveState(bool state)
        {
            _active = state;
        }
    }
}