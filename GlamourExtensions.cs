using Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WizGunCosmeticsAPI
{
    public static class GlamourExtensions
    {
        public static Dictionary<string, SkinData> emptySkinData = new Dictionary<string, SkinData>();
        private static Texture2D emptyTexture = new Texture2D(8, 8);

        public static Glamour AddIcon(this Glamour glamour, string iconPath)
        {
            glamour.icon = Helpers.CreateSpriteFromPNG(iconPath, string.Format("{0}-{1}", glamour.itemType, glamour.itemCode), 75);
            return glamour;
        }

        public static Glamour AddSkin(this Glamour glamour, SkinID skinID, string imgPath = "")
        {
            SkinData skinData;
            string itemType = new Regex(@"[A-Z][a-z]+").Match(skinID.ToString()).Value;
            if (string.IsNullOrEmpty(imgPath))
            {
                string name = string.Format("skin-{0}-modded-empty-{2}", itemType.ToLower(), Regex.Replace(skinID.ToString().Substring(itemType.Length), "([a-z])([A-Z])", "$1-$2").ToLower());
                if (!emptySkinData.TryGetValue(name, out skinData))
                {
                    skinData = ScriptableObject.CreateInstance<SkinData>();
                    skinData.skinID = skinID;
                    skinData.name = name;
                    skinData.skin = Sprite.Create(emptyTexture, new Rect(0, 0, 8, 8), new Vector2(0.5F, 0.5F), 200);
                    emptySkinData.Add(name, skinData);
                }
            }
            else
            {
                skinData = ScriptableObject.CreateInstance<SkinData>();
                skinData.skinID = skinID;
                skinData.name = string.Format("skin-{0}-{1}-{2}", itemType.ToLower(), glamour.itemCode, Regex.Replace(skinID.ToString().Substring(itemType.Length), "([a-z])([A-Z])", "$1-$2").ToLower());
                skinData.skin = Helpers.CreateSpriteFromPNG(imgPath, skinData.name, 200);
            }
            glamour.skins.Add(skinData);
            return glamour;
        }
    }
}
