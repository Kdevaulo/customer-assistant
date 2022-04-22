using System;
using UnityEngine;
using UnityEngine.UI;

namespace Dummy
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _image;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Text _itemDescriptionText;
        [SerializeField] private Toggle _infoToggle;
        [SerializeField] private GameObject _descriptionPanel;
        public GameObject DescriptionPanel => _descriptionPanel;
        public Toggle InfoToggle => _infoToggle;
        public event Action NextClick;
        public event Action PreviousClick;
        public event Action<bool> InfoClick;

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (Exception e)
            {
                enabled = false;
                throw e;
            }
            _nextButton.onClick.AddListener(OnNextClick);
            _previousButton.onClick.AddListener(OnPreviousClick);
            _infoToggle.onValueChanged.AddListener(OnInfoClick);
        }

        private void OnDisable()
        {
            _nextButton?.onClick.RemoveListener(OnNextClick);
            _previousButton?.onClick.RemoveListener(OnPreviousClick);
            _infoToggle.onValueChanged.RemoveListener(OnInfoClick);
        }

        private void Validate()
        {
            if (_image == null)
                throw new InvalidOperationException();
        }

        public void SetIcon(Sprite image)
        {
            _image.sprite = image;
        }

        public void SetDescriptionText(Item item)
        {
            _itemDescriptionText.text = $"{item.Name}\n{item.ShopName}\n{item.Price}";
        }

        private void OnNextClick()
        {
            NextClick?.Invoke();
        }

        private void OnPreviousClick()
        {
            PreviousClick?.Invoke();
        }

        private void OnInfoClick(bool clicked)
        {
            clicked = _infoToggle.isOn;
            InfoClick?.Invoke(clicked);
        }
    }
}