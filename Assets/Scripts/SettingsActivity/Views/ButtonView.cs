using System;

using UnityEngine;
using UnityEngine.EventSystems;

namespace SettingsActivity.Views
{
    [AddComponentMenu(nameof(ButtonView) + " in SettingsActivity")]
    [RequireComponent(typeof(Collider2D))]
    public class ButtonView : MonoBehaviour, IPointerClickHandler
    {
        public event Action ButtonClicked = delegate { };

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            ButtonClicked.Invoke();
        }
    }
}