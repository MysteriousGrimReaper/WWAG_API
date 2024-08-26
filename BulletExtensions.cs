using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WizGunBulletAPI
{
    public static class BulletExtensions
    {
        public static Bullet AddIcon(this Bullet bullet, string iconPath)
        {
            bullet.icon = Helpers.CreateSpriteFromPNG(iconPath, string.Format("{0}-{1}", bullet.bulletType, bullet.bulletTier), 75f);
            return bullet;
        }
    }
}
