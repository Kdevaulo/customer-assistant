using System;
using System.Collections.Generic;

using CustomerAssistant.DummyActivity;

using Cysharp.Threading.Tasks;

using Mapbox.Json;

using TMPro;

using UnityEngine;
using UnityEngine.Experimental.Rendering;

using Object = UnityEngine.Object;

namespace CustomerAssistant
{
    public static class Utils
    {
        public static void GetFavoritesFromPrefs(List<Product> products, Action<string, Product> action)
        {
            products.Clear();

            for (int i = 0; i < Constants.MaxFavorites; i++)
            {
                // todo: remove key dependency on index using route table
                var key = string.Format(Constants.PrefsKeyPattern, i);

                if (!PlayerPrefs.HasKey(key))
                {
                    break;
                }

                var serializedString = PlayerPrefs.GetString(key);

                var product = JsonConvert.DeserializeObject<Product>(serializedString);

                product.Sprite = ConvertSprite(product.Image);

                action.Invoke(key, product);
            }
        }

        public static Sprite ConvertSprite(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            Texture2D tex = new Texture2D(1, 1, GraphicsFormat.R8_UNorm, TextureCreationFlags.None);
            tex.LoadImage(imageBytes);

            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0f), 100, 1, SpriteMeshType.FullRect);

            return sprite;
        }

        public static void CreateNotification(string message, TextMeshProUGUI notificationPrefab,
            RectTransform parent)
        {
            var notification = Object.Instantiate(notificationPrefab, parent);

            notification.text = message;

            DestroyAfterTime(Constants.NotificationSecondsLifeTime, notification.gameObject).Forget();
        }

        private static async UniTask DestroyAfterTime(int seconds, GameObject target)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds));

            if (target) // todo: change to CTS using
            {
                target.SetActive(false);

                Object.Destroy(target);
            }
        }
    }
}