using System;
using System.Collections.Generic;
using System.Linq;

using Mapbox.Json;
using Mapbox.Unity.Map;
using Mapbox.Utils;

using UnityEngine;

namespace CustomerAssistant.MapKit
{
    public class ShopPresenter : MonoBehaviour
    {
        [SerializeField]
        private MarkerCreator _markerCreator;

        [SerializeField]
        private AbstractMap _map;

        [SerializeField]
        private GameObject _markerPrefab;

        [SerializeField]
        private ShopData[] _shopData;

        [SerializeField]
        private float _spawnScale = 50f;

        private Vector2d[] _locations;

        private List<Transform> _spawnedObjects;

        private void Start()
        {
            _locations = _shopData.Select(t => t.latLong).ToArray();

            _spawnedObjects = new List<Transform>();

            for (int i = 0; i < _locations.Length; i++)
            {
                var instance = Instantiate(_markerPrefab);

                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i]);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

                _spawnedObjects.Add(instance.transform);
            }
        }

        private void OnEnable()
        {
            _markerCreator.Created += OnCreated;
        }

        private void OnDisable()
        {
            _markerCreator.Created -= OnCreated;
        }

        private void Update()
        {
            for (int i = 0; i < _spawnedObjects.Count; i++)
            {
                var spawnedObject = _spawnedObjects[i];

                spawnedObject.localPosition = _map.GeoToWorldPosition(_locations[i]);
                spawnedObject.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }

        private void OnCreated(Vector2d latlong)
        {
            var list = new List<string>();

            var radius = _map.WorldToGeoPosition(_markerCreator.Radius);

            var r = Vector2d.Distance(latlong, radius);

            for (int i = 0; i < _shopData.Length; i++)
            {
                var distanceToShop = Vector2d.Distance(latlong, _locations[i]);

                if (distanceToShop < r)
                {
                    list.Add(_shopData[i].name);
                }
            }

            var shopJson = new ShopJson {Shops = list};

            var json = JsonConvert.SerializeObject(shopJson);

            PlayerPrefs.SetString("Shops", json);
            PlayerPrefs.Save();
        }
    }

    [Serializable]
    public class ShopJson
    {
        public List<string> Shops { get; set; }

        public string this[int index]
        {
            get => Shops[index];
            set => Shops[index] = value;
        }
    }
}