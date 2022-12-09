using System;

using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.MapKit
{
    public class RingSliderView : MonoBehaviour
    {
        public event Action<float> SliderValueChanged = delegate { };

        public float SliderValue => _slider.value;

        [SerializeField] private Slider _slider;

        public void SetValueWithoutNotify(float value)
        {
            if (value > _slider.maxValue)
            {
                value = _slider.maxValue;
            }

            if (value < _slider.minValue)
            {
                value = _slider.minValue;
            }

            _slider.SetValueWithoutNotify(value);
        }

        private void Awake()
        {
            _slider.onValueChanged.AddListener(HandleSliderValueChange);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(HandleSliderValueChange);
        }

        private void HandleSliderValueChange(float value)
        {
            SliderValueChanged.Invoke(value);
        }
    }
}