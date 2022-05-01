using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dummy
{
    public class DummyMainView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Toggle[] _infoToggles;

        public void OnPointerClick(PointerEventData eventData)
        {
            CloseInfoPanels();
        }

        public void CloseInfoPanels()
        {
            foreach(Toggle toggle in _infoToggles)
            {
                toggle.isOn = false;
            }
        }
    }
}