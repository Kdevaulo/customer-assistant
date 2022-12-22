using System;

using CustomerAssistant.DummyActivity;
using CustomerAssistant.MapKit;

using Cysharp.Threading.Tasks;

using Mapbox.Json;

using UnityEngine;
using UnityEngine.Networking;

namespace CustomerAssistant.DatabaseLoadSystem
{
    public static class DataLoader
    {
        public static event Action ErrorOnLoad = delegate { };

        private static ShopData[] ShopDataCollection;

        private static Product[] Products;

        public static async UniTask LoadShopDataAsync()
        {
            var loadedString = string.Empty;

            using (var webRequest = UnityWebRequest.Get("http://customer-assistant.site/getShops.php"))
            {
                webRequest.SendWebRequest();

                await UniTask.WaitUntil(() => webRequest.isDone);

                if (webRequest.error != null)
                {
                    ErrorOnLoad.Invoke();
                }
                else
                {
                    loadedString = webRequest.downloadHandler.text;
                }
            }

            ShopDataCollection = JsonConvert.DeserializeObject<ShopData[]>(loadedString);
        }

        public static async UniTask LoadItemsByShopIDAsync(string shopIDs)
        {
            var loadedString = string.Empty;

            WWWForm form = new WWWForm();

            form.AddField("id", shopIDs);

            using (UnityWebRequest webRequest =
                   UnityWebRequest.Post("http://customer-assistant.site/getProducts.php", form))
            {
                webRequest.SendWebRequest();

                await UniTask.WaitUntil(() => webRequest.isDone);

                if (webRequest.error != null)
                {
                    ErrorOnLoad.Invoke();
                    return;
                }

                loadedString = webRequest.downloadHandler.text;
            }

            // note: needs cz bool in database looks like [0,1](int)
            loadedString = loadedString.Replace("\"sale\":\"1\"", "\"sale\":\"true\"");
            loadedString = loadedString.Replace("\"sale\":\"0\"", "\"sale\":\"false\"");

            Products = JsonConvert.DeserializeObject<Product[]>(loadedString);

            foreach (var product in Products)
            {
                product.Sprite = Utils.ConvertSprite(product.Image);
            }
        }

        public static ShopData[] GetShopData()
        {
            return ShopDataCollection;
        }

        public static Product[] GetProducts()
        {
            return Products;
        }
    }
}