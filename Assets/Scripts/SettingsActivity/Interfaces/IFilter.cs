namespace CustomerAssistant.SettingsActivity
{
    public interface IFilter
    {
        bool Active { get; }

        string Name { get; }

        void ChangeActiveState(bool state);
    }
}