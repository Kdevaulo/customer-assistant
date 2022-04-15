using System.Collections.Generic;

namespace SettingsActivity
{
    public interface IFiltersModel
    {
        List<IFilter> GetFilters();
        
        void AddFilter(IFilter filter);
        
        void RemoveFilter(IFilter filter);
        
        void ClearFilters();
    }
}