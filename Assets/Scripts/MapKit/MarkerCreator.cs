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

        private const float MapScaleCoefficient = 0.01f;

        private MarkerView _currentMarker;

        private Vector2d _geoPosition;

        private Vector2d _cachedRingPointPosition;

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
            _map.OnUpdated -= HandleMapScaleUpdate;
        }

        private void HandleMapScaleUpdate()
        {
            if (_currentMarker)
            {
                var pointPosition = _map.GeoToWorldPosition(_cachedRingPointPosition);

                var distance = Vector3.Distance(_currentMarker.transform.localPosition, pointPosition) *
                               MapScaleCoefficient;

                _currentMarker.RingTransform.localScale =
                    _markerPrefab.RingTransform.localScale = new Vector2(distance, distance);

                SetSliderValue(distance);
            }
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

            _currentMarker.RingTransform.localScale = new Vector2(axisValue, axisValue);

            _ringSliderView.SliderValueChanged -= HandleSliderValueChange;
            _ringSliderView.SliderValueChanged += HandleSliderValueChange;

            CacheRingPoint();

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

            CacheRingPoint();

            Created.Invoke(_geoPosition);
        }

        private void CacheRingPoint()
        {
            _cachedRingPointPosition = _map.WorldToGeoPosition(RadiusPointPosition);
        }

        private float ConvertSliderValue(float value)
        {
            return (value + 1) * 0.1f;
        }

        private void SetSliderValue(float value)
        {
            _ringSliderView.SetValueWithoutNotify(value * 10 - 1);
        }
    }
}