using System;
using System.Collections.Generic;
using System.Linq;

using CustomerAssistant.DatabaseLoadSystem;

using Cysharp.Threading.Tasks;

using Mapbox.Json;
using Mapbox.Unity.Map;
using Mapbox.Utils;

using UnityEngine;

namespace CustomerAssistant.MapKit
{
    public class ShopPresenter : MonoBehaviour
    {
        [SerializeField] private MarkerCreator _markerCreator;

        [SerializeField] private AbstractMap _map;

        [SerializeField] private GameObject _markerPrefab;

        [SerializeField] private float _spawnScale = 50f;

        private ShopData[] _shopData;

        private Vector2d[] _locations;

        private List<Transform> _spawnedObjects;

        private void OnEnable()
        {
            _markerCreator.Created += OnCreated;
        }

        private void OnDisable()
        {
            _markerCreator.Created -= OnCreated;
        }

        private void Start()
        {
            InitializeAsync().Forget();
        }

        private void Update()
        {
            if (_spawnedObjects == null)
            {
                return;
            }

            for (int i = 0; i < _spawnedObjects.Count; i++)
            {
                var spawnedObject = _spawnedObjects[i];

                spawnedObject.localPosition = _map.GeoToWorldPosition(_locations[i]);
                spawnedObject.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }

        private async UniTask InitializeAsync()
        {
            await SetShopDataAsync();

            InitializePoints();
        }

        private async UniTask SetShopDataAsync()
        {
            await DataLoader.LoadShopDataAsync();

            _shopData = DataLoader.GetShopData();
        }

        private void InitializePoints()
        {
            _locations = _shopData.Select(t => new Vector2d(t.Latitude, t.Longitude)).ToArray();

            _spawnedObjects = new List<Transform>();

            for (int i = 0; i < _locations.Length; i++)
            {
                var instance = Instantiate(_markerPrefab);

                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i]);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

                _spawnedObjects.Add(instance.transform);
            }
        }

        private void OnCreated(Vector2d latlong)
        {
            var list = new List<int>();

            var pointPosition = _map.GeoToWorldPosition(latlong);

            var pointAndRadiusPointDistance = Vector3.Distance(pointPosition, _markerCreator.RadiusPointPosition);

            for (int i = 0; i < _shopData.Length; i++)
            {
                var distanceToShop = Vector3.Distance(pointPosition, _map.GeoToWorldPosition(_locations[i]));

                if (distanceToShop < pointAndRadiusPointDistance)
                {
                    list.Add(_shopData[i].ID);
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
        public List<int> Shops { get; set; }

        public int this[int index]
        {
            get => Shops[index];
            set => Shops[index] = value;
        }
    }
}