namespace SettingsActivity
{
    public interface IFilter
    {
        bool Enabled { get; }

        string Name { get; }

        void ChangeEnabledState(bool state);
    }
}