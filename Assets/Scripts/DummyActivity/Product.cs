using System;

using UnityEngine;

namespace CustomerAssistant.DummyActivity
{
    [Serializable]
    public class Product
    {
        public string Name;

        public string Size;

        public int Price;

        public string Color;

        public string Material;

        public bool Sale;

        public Clothes Type;

        public string Shop_Name;

        public string Address;

        public string Site;

        public string Latitude;

        public string Longitude;

        public string Image;

        [NonSerialized]
        public Sprite Sprite;

        public bool Delivery;
    }
}