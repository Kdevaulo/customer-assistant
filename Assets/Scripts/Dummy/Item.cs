using System.Collections.Generic;
using UnityEngine;
using SettingsActivity.Models;

namespace Dummy
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _price;
        [SerializeField] private string _shopName;
        [SerializeField] private string _color;
        [SerializeField] private string _material;
        [SerializeField] private bool _sale;
        [SerializeField] private bool _delivery;
        [SerializeField] private List<StringBoolModel> _size = new List<StringBoolModel>();

        public string Name => _name;
        public Sprite Image => _image;
        public int Price => _price;
        public string ShopName => _shopName;
        public string Color => _color;
        public string Material => _material;
        public bool Sale => _sale;
        public bool Delivery => _delivery;
        public List<StringBoolModel> Size => _size;
    }
}