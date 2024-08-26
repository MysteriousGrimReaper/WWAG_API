using System;
using System.IO;
using UnityEngine;

namespace WizGunBulletAPI
{
    // Token: 0x02000006 RID: 6
    public static class Helpers
    {
        // Token: 0x0600000F RID: 15 RVA: 0x00002580 File Offset: 0x00000780
        public static Sprite CreateSpriteFromPNG(string iconPath, string name, float ppu)
        {
            Texture2D texture2D = new Texture2D(2, 2);
            byte[] data = File.ReadAllBytes(iconPath);
            texture2D.LoadImage(data);
            texture2D.name = name;
            Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), ppu);
            sprite.name = name;
            return sprite;
        }
    }
}
