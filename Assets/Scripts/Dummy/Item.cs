using UnityEngine;

namespace Dummy
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public string ItemName => _itemName;
        public Sprite ItemImage => _itemIMage;
        public float ItemPrice => _price;
        public string ShopName => _shopName;

        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemIMage;
        [SerializeField] private float _price;
        [SerializeField] private string _shopName;
    }
}