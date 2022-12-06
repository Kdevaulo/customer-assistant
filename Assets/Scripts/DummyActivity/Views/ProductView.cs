using System;

using CustomerAssistant.DummyActivity;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace DummyActivity.Views
{
    public class ProductView : MonoBehaviour
    {
        public event Action NextClick;
        public event Action PreviousClick;
        public event Action<bool> InfoClick;

        public ProductDetailedDescriptionView DescriptionPanel => _descriptionPanel;
        public Toggle InfoToggle => _infoToggle;

        public Image ButtonImage => _buttonImage;

        public Transform ObjectsParent => _objectsParent;
        public int RendererSortingOrder => _rendererSortingOrder;

        [SerializeField] private Button _nextButton;

        [SerializeField] private Button _previousButton;

        [SerializeField] private Image _buttonImage;

        [SerializeField] private Text _itemDescriptionText;

        [SerializeField] private Toggle _infoToggle;

        [SerializeField] private ProductDetailedDescriptionView _descriptionPanel;

        [SerializeField] private Transform _objectsParent;

        [SerializeField] private int _rendererSortingOrder;

        private SpriteRenderer[] _itemObjects;

        private void OnEnable()
        {
            _buttonImage.alphaHitTestMinimumThreshold = 0.1f;

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

        public void SetItemObjects(SpriteRenderer[] renderers)
        {
            _itemObjects = renderers;
        }

        public void SetButtonShape(Sprite image)
        {
            _buttonImage.sprite = image;
            _buttonImage.SetNativeSize();
        }

        public void SetDescriptionText(Product item)
        {
            _itemDescriptionText.text = $"{item.Name}\n{item.Shop_Name}\n{item.Price}";
        }

        public void EnableItemObject(Sprite image)
        {
            Assert.IsNotNull(_itemObjects);

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