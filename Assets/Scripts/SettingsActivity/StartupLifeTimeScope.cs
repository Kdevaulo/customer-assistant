using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity
{
    [AddComponentMenu(nameof(StartupLifeTimeScope) + " in SettingsActivity")]
    public class StartupLifeTimeScope : MonoBehaviour
    {
        [SerializeField] private BackButtonView _backButton;

        private void Start()
        {
            _ = new GameplayController(_backButton);
        }
    }
}