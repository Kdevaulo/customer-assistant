using System;

using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace CustomerAssistant
{
    public static class Utils
    {
        public static Sprite ConvertSprite(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            Texture2D tex = new Texture2D(1, 1, GraphicsFormat.R8_UNorm, TextureCreationFlags.None);
            tex.LoadImage(imageBytes);

            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0f), 100, 1, SpriteMeshType.FullRect);

            return sprite;
        }
    }
}