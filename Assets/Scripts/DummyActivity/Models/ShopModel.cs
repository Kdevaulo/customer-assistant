using System;

using UnityEngine;

namespace DummyActivity.Models
{
    [Serializable]
    public class ShopModel
    {
        [SerializeField] private string _name;
        [SerializeField] private string _address;

        public string Name => _name;
        public string Address => _address;
    }
}