using System;

using UnityEngine;
using UnityEngine.UI;

using DummyActivity.Configs;

namespace DummyActivity.Views
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _itemObjects;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;

        [SerializeField] private Image _buttonImage;
        [SerializeField] private Text _itemDescriptionText;
        [SerializeField] private Toggle _infoToggle;

        [SerializeField] private ProductDetailedDescriptionView _descriptionPanel;

        public event Action NextClick;
        public event Action PreviousClick;
        public event Action<bool> InfoClick;

        public ProductDetailedDescriptionView DescriptionPanel => _descriptionPanel;
        public Toggle InfoToggle => _infoToggle;
        public Image ButtonImage => _buttonImage;

        public void SetButtonShape(Sprite image)
        {
            _buttonImage.sprite = image;
            _buttonImage.SetNativeSize();
        }

        public void SetDescriptionText(Product item)
        {
            _itemDescriptionText.text = $"{item.Name}\n{item.Shop.Name}\n{item.Price}";
        }

        public void EnableItemObject(Sprite image)
        {
            for (int i = 0; i < _itemObjects.Length; i++)
            {
                if (_itemObjects[i].sprite == image)
                {
                    _itemObjects[i].gameObject.SetActive(true);
                }
                else
                {
                    _itemObjects[i].gameObject.SetActive(false);
                }
            }
        }

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
            if (_itemObjects == null)
                throw new InvalidOperationException();

            _buttonImage.alphaHitTestMinimumThreshold = 0.1f;
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