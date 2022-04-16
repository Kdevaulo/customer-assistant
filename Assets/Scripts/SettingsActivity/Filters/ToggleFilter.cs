using System;
using System.Collections.Generic;

using UnityEngine;

namespace SettingsActivity.Filters
{
    [Serializable]
    public class ToggleFilter : IFilter
    {
        public bool Enabled => _enabled;

        public string Name => _name;

        [SerializeField] private string _name;

        [SerializeField] private bool _enabled;

        [SerializeField] private List<string> _value;

        public void ChangeEnabledState(bool state)
        {
            _enabled = state;
        }
    }
}