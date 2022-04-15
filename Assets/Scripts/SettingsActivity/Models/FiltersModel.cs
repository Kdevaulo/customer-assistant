using System.Collections.Generic;

namespace SettingsActivity.Models
{
    public class FiltersModel : IFiltersModel
    {
        private List<IFilter> _filters = new List<IFilter>();

        public List<IFilter> GetFilters()
        {
            return _filters;
        }

        public void AddFilter(IFilter filter)
        {
            _filters.Add(filter);
        }

        public void RemoveFilter(IFilter filter)
        {
            _filters.Remove(filter);
        }

        public void ClearFilters()
        {
            _filters.Clear();
        }
    }
}