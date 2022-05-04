using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DummyActivity.Views
{
    public class DummyMainView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Toggle[] _infoToggles;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            CloseInfoPanels();
        }

        public void CloseInfoPanels()
        {
            foreach (Toggle toggle in _infoToggles)
            {
                toggle.isOn = false;
            }
        }
    }
}