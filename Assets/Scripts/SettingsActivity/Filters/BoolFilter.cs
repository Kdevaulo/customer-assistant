using System;

using UnityEngine;

namespace SettingsActivity.Filters
{
    [Serializable]
    public class BoolFilter : IFilter
    {
        public bool Enabled => _enabled;

        public string Name => _name;

        [SerializeField] private string _name;

        [SerializeField] private bool _enabled;

        [SerializeField] private bool _value;

        public void ChangeEnabledState(bool state)
        {
            _enabled = state;
        }
    }
}