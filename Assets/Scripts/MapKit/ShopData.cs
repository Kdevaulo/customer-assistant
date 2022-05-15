using System;

using Mapbox.Utils;

namespace CustomerAssistant.MapKit
{
    [Serializable]
    public struct ShopData
    {
        public Vector2d latLong;

        public string name;
    }
}