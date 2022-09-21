using System;

using Mapbox.Examples;
using Mapbox.Unity.Map;
using Mapbox.Utils;

using UnityEngine;

namespace CustomerAssistant.MapKit
{
    public class MarkerCreator : MonoBehaviour
    {
        public event Action<Vector2d> Created = delegate { };

        public Vector3 RadiusPointPosition => _currentMarker.RadiusPointPosition;

        [SerializeField] private RingSliderView _ringSliderView;

        [SerializeField] private MarkerView _markerPrefab;

        [SerializeField] private float _scale = 50;

        [SerializeField] private AbstractMap _map;

        [SerializeField] private QuadTreeCameraMovement _cameraMover;

        private MarkerView _currentMarker;

        private Vector2d _geoPosition;

        private float _searchRadiusMapCoefficient;

        private void OnValidate()
        {
            if (_map == null)
                _map = FindObjectOfType<AbstractMap>();

            if (_cameraMover == null)
                _cameraMover = FindObjectOfType<QuadTreeCameraMovement>();
        }

        private void OnEnable()
        {
            _cameraMover.Clicked += Create;
            _map.OnUpdated += HandleMapScaleUpdate;
        }

        private void OnDisable()
        {
            _cameraMover.Clicked -= Create;
        }

        private void HandleMapScaleUpdate()
        {
            var axisValue = (_map.Zoom - _searchRadiusMapCoefficient); // Todo: поправить коэффициент как-то
            
            _currentMarker.RingTransform.localScale = new Vector2(axisValue, axisValue);
            
            Debug.Log(_currentMarker.RingTransform.localScale);
        }

        private void Create(Vector2d latlong)
        {
            if (_currentMarker != null)
                Destroy(_currentMarker.gameObject);

            _geoPosition = latlong;

            _currentMarker = Instantiate(_markerPrefab);

            _currentMarker.transform.localPosition = _map.GeoToWorldPosition(_geoPosition);
            _currentMarker.transform.localScale = new Vector3(_scale, _scale, _scale);

            var axisValue = ConvertSliderValue(_ringSliderView.SliderValue);

            _searchRadiusMapCoefficient = _map.Zoom - axisValue;

            _currentMarker.RingTransform.localScale = new Vector2(axisValue, axisValue);

            _ringSliderView.SliderValueChanged -= HandleSliderValueChange;
            _ringSliderView.SliderValueChanged += HandleSliderValueChange;

            Created.Invoke(_geoPosition);
        }

        private void Update()
        {
            if (_currentMarker == null)
                return;

            _currentMarker.transform.localPosition = _map.GeoToWorldPosition(_geoPosition);
            _currentMarker.transform.localScale = new Vector3(_scale, _scale, _scale);
        }

        private void HandleSliderValueChange(float value)
        {
            var axisValue = ConvertSliderValue(value);

            var radius = new Vector2(axisValue, axisValue);
            _currentMarker.RingTransform.localScale = _markerPrefab.RingTransform.localScale = radius;

            Created.Invoke(_geoPosition);
        }

        private float ConvertSliderValue(float value)
        {
            return (value + 1) * 0.1f;
        }
    }
}