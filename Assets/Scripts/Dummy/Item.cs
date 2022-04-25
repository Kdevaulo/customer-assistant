using UnityEngine;

namespace Dummy
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private float _price;
        [SerializeField] private string _shopName;

        public string Name => _name;
        public Sprite Image => _image;
        public float Price => _price;
        public string ShopName => _shopName;
    }
}