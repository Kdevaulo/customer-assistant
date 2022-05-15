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

        public Vector3 Radius => _currentMarker.Radius;

        [SerializeField]
        private MarkerView _markerPrefab;

        [SerializeField]
        private float _scale = 100;

        [SerializeField]
        private AbstractMap _map;

        [SerializeField]
        private QuadTreeCameraMovement _cameraMover;

        private MarkerView _currentMarker;

        private Vector2d _geoPosition;

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
        }

        private void OnDisable()
        {
            _cameraMover.Clicked -= Create;
        }

        private void Create(Vector2d latlong)
        {
            if (_currentMarker != null)
                Destroy(_currentMarker.gameObject);

            _geoPosition = latlong;

            _currentMarker = Instantiate(_markerPrefab);

            _currentMarker.transform.localPosition = _map.GeoToWorldPosition(_geoPosition);
            _currentMarker.transform.localScale = new Vector3(_scale, _scale, _scale);

            Created.Invoke(_geoPosition);
        }

        private void Update()
        {
            if (_currentMarker == null)
                return;

            _currentMarker.transform.localPosition = _map.GeoToWorldPosition(_geoPosition);
            _currentMarker.transform.localScale = new Vector3(_scale, _scale, _scale);
        }
    }
}