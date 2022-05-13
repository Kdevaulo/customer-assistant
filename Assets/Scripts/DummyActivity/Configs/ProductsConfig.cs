using System.Collections.Generic;

using UnityEngine;

namespace DummyActivity.Configs
{
    [CreateAssetMenu(fileName = nameof(ProductsConfig), menuName = "DummyActivity/Configs/" + nameof(ProductsConfig))]
    public class ProductsConfig : ScriptableObject
    {
        [SerializeField] private List<Product> _products = new List<Product>();

        public List<Product> GetProductsOfType(Product.ClothesType type)
        {
            List<Product> products = new List<Product>();

            foreach (Product product in _products)
            {
                if (product.Type == type)
                {
                    products.Add(product);
                }
            }

            return products;
        }
    }
}