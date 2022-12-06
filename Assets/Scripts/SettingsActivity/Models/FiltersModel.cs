using System.Collections.Generic;
using System.Linq;

namespace CustomerAssistant.SettingsActivity.Models
{
    public class FiltersModel
    {
        private readonly List<IFilter> _filters;

        public FiltersModel(List<IFilter> filters)
        {
            _filters = filters;
        }

        public List<IFilter> GetActiveFilters()
        {
            return _filters?.Where(x => x.Active).ToList();
        }

        public List<IFilter> GetInactiveFilters()
        {
            return _filters?.Where(x => !x.Active).ToList();
        }

        public List<IFilter> GetFilters()
        {
            return _filters;
        }
    }
}