using UnityEngine;
using UnityEngine.UI;

namespace Dummy
{
    [RequireComponent(typeof(Button))]
    public class BackButton : MonoBehaviour
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