using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DummyActivity.Views
{
    public class DummyMainView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Toggle[] _infoToggles;

        [SerializeField] private Text _message;

        public event Action SettingsButtonClicked = delegate { };

        public event Action MapButtonClicked = delegate { };
        public event Action FavoritesButtonClicked = delegate { };

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

        public void HandleSettingsButtonClick()
        {
            SettingsButtonClicked.Invoke();
        }

        public void HandleMapButtonClick()
        {
            MapButtonClicked.Invoke();
        }

        public void HandleFavoritesButtonClick()
        {
            FavoritesButtonClicked.Invoke();
        }
    }
}