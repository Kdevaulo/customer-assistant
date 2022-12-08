namespace CustomerAssistant.DummyActivity.Models
{
    public class FavoritesModel
    {
        private Product _cachedProduct;

        public void CacheFavoriteProduct(Product product)
        {
            _cachedProduct = product;
        }

        public Product GetFavoriteProduct()
        {
            return _cachedProduct;
        }
    }
}