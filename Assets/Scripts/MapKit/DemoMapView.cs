using System.Globalization;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.MapKit
{
    public class DemoMapView : MonoBehaviour
    {
        [SerializeField]
        private RawImage _mapView;

        [SerializeField]
        private Vector2 _ll = new Vector2(37.620070f, 55.753630f);

        [SerializeField]
        private Vector2Int _size = new Vector2Int(600, 450);

        [SerializeField]
        private int _zoom = 1;

        [SerializeField]
        private YandexStaticMap.YandexMapLayer _mapLayer = YandexStaticMap.YandexMapLayer.Map;

        private IStaticMap _staticMap;

        private void Awake()
        {
            _staticMap = GetYandexStaticMap();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        private void OnValidate()
        {
            if (!_mapView)
                _mapView = GetComponent<RawImage>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetYandexStaticMap()
                    .Build(_ll, _size)
                    .ContinueWith(texture => { _mapView.texture = texture; });
            }
        }

        private IStaticMap GetYandexStaticMap()
        {
            return new YandexStaticMap()
                .SetMapLayer(_mapLayer)
                .SetZoomLevel(_zoom);
        }
    }
}