namespace CustomerAssistant.SettingsActivity
{
    public interface ITypedFilter<T> : IFilter
    {
        void ChangeValue(T value);
    }
}