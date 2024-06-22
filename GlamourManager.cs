using Skins;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizGunCosmeticsAPI
{
    public static class GlamourManager
    {
        private static List<Glamour> glamours = new List<Glamour>();
        internal static List<GameObject> glamourGameObjects { get { return glamours.Select(x => x.gameObject).ToList(); } }
        internal static List<GameObject> eyeGameObjects { get { return glamours.Where(x => x.itemType == ItemType.Eyes).Select(x => x.gameObject).ToList(); } }
        internal static List<SkinGroup> glamourSkinGroups { get { return glamours.Select(x => x.equipment.skinGroup).ToList(); } }
        internal static List<RecipeBase> glamourRecipeBases { get { return glamours.Where(x => x.recipe != null).Select(x => x.recipe).ToList<RecipeBase>(); } }
        internal static List<GlamourRecipe> glamourRecipes { get { return glamours.Where(x => x.recipe != null).Select(x => x.recipe).ToList(); } }

        public static Glamour New(ItemType itemType, string itemCode, string itemName, string itemDescription)
        {
            WizGunCosmeticsAPI.Log.LogInfo(string.Format("Adding Glamour {0}:{1}", itemType.ToString(), itemCode));
            Glamour glamour = new Glamour(itemType, itemCode, itemName, itemDescription);
            glamours.Add(glamour);
            return glamour;
        }

        public static void CreateUnityObjects()
        {
            glamours.ForEach(x => x.CreateUnityObject());
        }
    }
}
