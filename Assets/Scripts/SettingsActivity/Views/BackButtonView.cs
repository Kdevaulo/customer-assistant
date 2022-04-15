using System;

using UnityEngine;
using UnityEngine.EventSystems;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(BackButtonView) + " in SettingsActivity")]
    public class BackButtonView : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnBackButtonClicked = delegate { };

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnBackButtonClicked.Invoke();
        }
    }
}