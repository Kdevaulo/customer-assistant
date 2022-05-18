using System;

using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.MapKit
{
    public class RingSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public event Action<float> SliderValueChanged = delegate { };

        public float SliderValue => _slider.value;
        
        private void Awake()
        {
            _slider.onValueChanged.AddListener(HandleSliderValueChange);
        }

        private void HandleSliderValueChange(float value)
        {
            SliderValueChanged.Invoke(value);
        }
    }
}