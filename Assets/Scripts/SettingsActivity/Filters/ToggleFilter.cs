using System;
using System.Collections.Generic;

using UnityEngine;

namespace SettingsActivity.Filters
{
    [Serializable]
    public class ToggleFilter : ITypedFilter<List<string>>
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _active;

        [SerializeField] private List<string> _value;

        public bool Active => _active;

        public string Name => _name;

        public List<string> Value => _value;

        public void ChangeActiveState(bool state)
        {
            _active = state;
        }

        public void ChangeValue(List<string> value)
        {
            _value = value;
        }
    }
}