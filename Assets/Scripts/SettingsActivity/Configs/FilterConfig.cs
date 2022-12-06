using System.Collections.Generic;

using CustomerAssistant.SettingsActivity.Filters;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Configs
{
    [CreateAssetMenu(fileName = nameof(FilterConfig), menuName = "SettingsActivity/Configs/" + nameof(FilterConfig))]
    public class FilterConfig : ScriptableObject
    {
        [SerializeField] private StringFilter _color;
        [SerializeField] private ToggleFilter _size;
        [SerializeField] private StringFilter _material;
        [SerializeField] private IntRangeFilter _price;
        [SerializeField] private BoolFilter _sale;
        [SerializeField] private StringFilter _shop;
        [SerializeField] private BoolFilter _delivery;

        public List<IFilter> GetFilters()
        {
            return new List<IFilter>
            {
                _color, _size, _material, _price, _sale, _shop, _delivery
            };
        }
    }
}