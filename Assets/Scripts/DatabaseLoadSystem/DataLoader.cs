using System;

using CustomerAssistant.DummyActivity;
using CustomerAssistant.MapKit;

using Cysharp.Threading.Tasks;

using Mapbox.Json;

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;

namespace CustomerAssistant.DatabaseLoadSystem
{
    public static class DataLoader
    {
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
                    Debug.LogError("Getting shops data failed: " + webRequest.error);
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
                    Debug.LogError($"Getting items data by shop IDs ({shopIDs}) failed: " + webRequest.error);
                }
                else
                {
                    loadedString = webRequest.downloadHandler.text;
                }
            }

            // todo: remove this shit after DB fix
            loadedString = loadedString.Replace("\"sale\":\"1\"", "\"sale\":\"true\"");
            loadedString = loadedString.Replace("\"sale\":\"0\"", "\"sale\":\"false\"");

            Products = JsonConvert.DeserializeObject<Product[]>(loadedString);

            foreach (var product in Products)
            {
                byte[] imageBytes = Convert.FromBase64String(product.Image);
                Texture2D tex = new Texture2D(1, 1, GraphicsFormat.R8_UNorm, TextureCreationFlags.None);
                tex.LoadImage(imageBytes);

                Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                    new Vector2(0.5f, 0f), 100, 1, SpriteMeshType.FullRect);

                product.Sprite = sprite;
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