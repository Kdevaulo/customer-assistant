using System;

using Mapbox.Utils;

namespace CustomerAssistant.MapKit
{
    [Serializable]
    public struct ShopData
    {
        public int ID;

        public double Latitude;

        public double Longitude;

        public string Shop_Name;

        public string Site;

        public string Address;
    }
}