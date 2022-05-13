using System;

using UnityEngine;

namespace DummyActivity.Models
{
    [Serializable]
    public class ShopModel
    {
        [SerializeField] private string _name;
        [SerializeField] private string _address;
        [SerializeField] private string _URL;

        public string Name => _name;
        public string Address => _address;

        public string GetShopURLString()
        {
            if (!string.IsNullOrEmpty(_URL))
            {
                return string.Format("<#7f7fe5><u><link=\"{0}\">{1}</link></u></color>", _URL, _name); ;
            }
            else return null;
        }
    }
}