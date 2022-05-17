using System;

using UnityEngine;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(ButtonView) + " in SettingsActivity")]
    public class ButtonView : MonoBehaviour
    {
        public event Action ButtonClicked = delegate { };

        public void HandleButtonClick()
        {
            ButtonClicked.Invoke();
        }
    }
}