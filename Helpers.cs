using System.IO;
using UnityEngine;

namespace WizGunCosmeticsAPI
{
    public static class Helpers
    {

        public static Sprite CreateSpriteFromPNG(string iconPath, string name, float ppu)
        {
            Texture2D texture = new Texture2D(2, 2);
            byte[] imgBytes = File.ReadAllBytes(iconPath);
            ImageConversion.LoadImage(texture, imgBytes);
            texture.name = name;
            texture.filterMode = FilterMode.Point;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5F, 0.5F), ppu);
            sprite.name = name;

            return sprite;
        }
    }
}
