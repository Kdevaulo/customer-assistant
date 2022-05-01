using System;

using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity.Filters
{
    [Serializable]
    public class IntRangeFilter : ITypedFilter<Vector2Int>
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _active;

        [SerializeField] private Vector2Int _value;

        [SerializeField] private IntRangeView _intRangeFilterPrefab;

        void IFilter.ChangeActiveState(bool state)
        {
            _active = state;
        }

        void ITypedFilter<Vector2Int>.ChangeValue(Vector2Int value)
        {
            _value = value;
        }

        public bool Active => _active;

        public string Name => _name;

        public Vector2Int Value => _value;

        public IntRangeView IntRangeFilterPrefab => _intRangeFilterPrefab;
    }
}