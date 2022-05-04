using System.Collections.Generic;

using UnityEngine;

using DummyActivity.Models;

using SettingsActivity.Models;

namespace DummyActivity.Configs
{
    [CreateAssetMenu(fileName = nameof(Product), menuName = "DummyActivity/Configs/" + nameof(Product))]
    public class Product : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _price;
        [SerializeField] private ShopModel _shop;
        [SerializeField] private string _color;
        [SerializeField] private string _material;
        [SerializeField] private bool _sale;
        [SerializeField] private bool _delivery;
        [SerializeField] private List<StringBoolModel> _size = new List<StringBoolModel>();
        [SerializeField] private ClothesType _type;

        public enum ClothesType
        {
            PANTS,
            SHIRTS,
            T_SHIRTS
        }

        public string Name => _name;
        public Sprite Image => _image;
        public int Price => _price;
        public ShopModel Shop => _shop;
        public string Color => _color;
        public string Material => _material;
        public bool Sale => _sale;
        public bool Delivery => _delivery;
        public List<StringBoolModel> Size => _size;
        public ClothesType Type => _type;
    }
}