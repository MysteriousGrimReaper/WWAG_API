using Skins;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WizGunCosmeticsAPI
{
    public static class GlamourExtensions
    {
        public static Glamour AddIcon(this Glamour glamour, string iconPath)
        {
            glamour.icon = Helpers.CreateSpriteFromPNG(iconPath, String.Format("{0}-{1}", glamour.itemType, glamour.itemCode), 75);
            return glamour;
        }

        public static Glamour AddSkin(this Glamour glamour, SkinID skinID, string imgPath)
        {
            SkinData skinData = ScriptableObject.CreateInstance<SkinData>();
            skinData.skinID = skinID;
            skinData.name = String.Format("{0}-{1}", glamour.itemCode, Regex.Replace(skinID.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower());
            skinData.skin = Helpers.CreateSpriteFromPNG(imgPath, skinData.name, 200);
            
            glamour.skins.Add(skinData);
            return glamour;
        }
    }
}
