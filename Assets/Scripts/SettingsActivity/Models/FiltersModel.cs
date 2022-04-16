using System.Collections.Generic;
using System.Linq;

namespace SettingsActivity.Models
{
    public class FiltersModel
    {
        private readonly List<IFilter> _filters;

        public FiltersModel(params IFilter[] filters)
        {
            _filters = filters.ToList();
        }

        public List<IFilter> GetActiveFilters()
        {
            return _filters?.Where(x => x.Enabled).ToList();
        }

        public List<IFilter> GetInactiveFilters()
        {
            return _filters?.Where(x => !x.Enabled).ToList();
        }

        public List<IFilter> GetFilters()
        {
            return _filters;
        }

        public bool EnableFilter(string name)
        {
            return ChangeFilterState(name, true);
        }

        public bool DisableFilter(string name)
        {
            return ChangeFilterState(name, false);
        }

        private bool ChangeFilterState(string name, bool state)
        {
            var filter = FindFilterByName(name);

            if (filter == null) return false;

            filter.ChangeEnabledState(state);

            return true;
        }

        private IFilter FindFilterByName(string name)
        {
            return _filters.FirstOrDefault(x => x.Name == name);
        }
    }
}