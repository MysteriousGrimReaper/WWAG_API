using Inventory.Unity;
using Skins;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace WizGunCosmeticsAPI
{
    public class Glamour
    {
        public GameObject gameObject;
        public StaticItem item;
        public StaticItemEquipmentComponent equipment;
        public GlamourRecipe recipe;
        public string itemCode;

        internal ItemType itemType;
        internal string itemName;
        internal string itemDescription;
        internal Sprite icon;
        internal List<SkinData> skins = new List<SkinData>();

        public Glamour(ItemType itemType, string itemCode, string itemName, string itemDescription)
        {
            this.itemType = itemType;
            this.itemCode = itemCode;
            this.itemName = itemName;
            this.itemDescription = itemDescription;
            ConfigureRecipe();
        }

        public void CreateUnityObject()
        {
            gameObject = new GameObject(String.Format("item-{0}-{1}", itemType.ToString().ToLower(), itemCode));
            item = gameObject.AddComponent<StaticItem>();
            equipment = gameObject.AddComponent<StaticItemEquipmentComponent>();
            ConfigureItem();
            ConfigureEquipment();
            recipe.glamour = gameObject;
        }

        private void ConfigureItem()
        {
            item.arcanaWorth = 50;
            item.canConvertToArcana = true;
            item.displayDescription = new DisplayString() { backupString = itemDescription };
            item.displayName = new DisplayString() { backupString = itemName };
            item.itemRarity = ItemRarity.Common;
            item.icon = icon;
        }

        private void ConfigureEquipment()
        {
            equipment.itemType = itemType;

            SkinGroup skinGroup = ScriptableObject.CreateInstance<SkinGroup>();
            skinGroup.colorSlots = new ColorData[0];
            skinGroup.name = String.Format("sg-{0}-{1}", itemType.ToString().ToLower(), itemCode);
            skinGroup.skins = skins.ToArray();

            equipment.skinGroup = skinGroup;
        }

        private void ConfigureRecipe()
        {
            recipe = ScriptableObject.CreateInstance<GlamourRecipe>();
            recipe.name = String.Format("recipe-glamour-{0}-{1}", itemType.ToString().ToLower(), itemCode);
            recipe.recipeGroup = 6;
            recipe.requiredArcana = 10;
            recipe.requirements = new RecipeAssetRequirement[0];
        }
    }
}
