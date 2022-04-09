using System;
using System.Web;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace CustomerAssistant.MapKit
{
    public class YandexStaticMap : IStaticMap
    {
        private const string Url = "https://static-maps.yandex.ru/1.x/?";

        private YandexMapLayer _mapLayer = YandexMapLayer.Map;

        private int _zoomLevel = 1;

        async UniTask<Texture> IStaticMap.Build(Vector2 ll, Vector2Int size)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("ll", $"{ll.x},{ll.y}");
            queryString.Add("size", $"{size.x},{size.y}");
            queryString.Add("z", $"{_zoomLevel}");
            queryString.Add("l", _mapLayer.ToString().ToLower());

            var uri = new Uri(Url + queryString);

            using (var request = UnityWebRequestTexture.GetTexture(uri))
            {
                await request.SendWebRequest();

                return DownloadHandlerTexture.GetContent(request);
            }
        }

        public YandexStaticMap SetMapLayer(YandexMapLayer layer)
        {
            _mapLayer = layer;

            return this;
        }

        public YandexStaticMap SetZoomLevel(int zoomLevel)
        {
            _zoomLevel = zoomLevel;

            return this;
        }

        public enum YandexMapLayer
        {
            /// <summary>
            /// Scheme of the area and names of geographical objects. [PNG]
            /// </summary>
            Map,

            /// <summary>
            /// Satellite photographed area. [JPG]
            /// </summary>
            Sat,

            /// <summary>
            /// Names of geographical objects. [PNG]
            /// </summary>
            Skl,

            /// <summary>
            /// Traffic jam layer. [PNG]
            /// </summary>
            Trf
        }
    }
}