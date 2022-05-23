using System.Collections.Generic;

using Mapbox.Json;

using UnityEngine;

using CustomerAssistant.MapKit;

namespace DummyActivity.Configs
{
    [CreateAssetMenu(fileName = nameof(ProductsConfig), menuName = "DummyActivity/Configs/" + nameof(ProductsConfig))]
    public class ProductsConfig : ScriptableObject
    {
        [SerializeField] private List<Product> _products = new List<Product>();

        public List<Product> GetProductsOfType(Product.ClothesType type)
        {
            List<Product> products = new List<Product>();

            string json = PlayerPrefs.GetString("Shops");

            ShopJson shops = JsonConvert.DeserializeObject<ShopJson>(json);

            foreach (Product product in _products)
            {
                foreach (string shop in shops.Shops)
                {
                    if (product.Type == type && product.Shop.Name == shop)
                    {
                        products.Add(product);
                    }
                }
            }

            return products.Count == 0 ? null : products;
        }
    }
}