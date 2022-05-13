using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DummyActivity.Views
{
    public class DummyMainView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Toggle[] _infoToggles;

        [SerializeField] private Text _message;

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

            _message.gameObject.SetActive(false);
        }

        public void ShowMessage(string text)
        {
            _message.text += text;
            _message.gameObject.SetActive(true);
        }
    }
}