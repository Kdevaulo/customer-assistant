using System.Collections.Generic;

using SettingsActivity.Filters;

using UnityEngine;

namespace SettingsActivity.Configs
{
    [CreateAssetMenu(fileName = nameof(FilterConfig), menuName = "SettingsActivity/Configs/" + nameof(FilterConfig))]
    public class FilterConfig : ScriptableObject
    {
        [SerializeField] private StringFilter _color;
        [SerializeField] private ToggleFilter _size;

        [SerializeField] private StringFilter _material;

        //todo: add pricefilter (Slider)
        [SerializeField] private BoolFilter _sale;
        [SerializeField] private StringFilter _shop;
        [SerializeField] private BoolFilter _delivery;

        public List<IFilter> GetFilters()
        {
            return new List<IFilter>
            {
                _color, /*_size,*/ _material, _sale, _shop, _delivery
            };
        }
    }
}