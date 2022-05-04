using UnityEngine;
using UnityEngine.UI;

namespace DummyActivity.Views
{
    [RequireComponent(typeof(Button))]
    public class BackButtonView : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;

        private Button _backButton;

        private void Awake()
        {
            _backButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(CloseMenu);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(CloseMenu);
        }

        private void CloseMenu()
        {
            _menu.SetActive(false);
        }
    }
}