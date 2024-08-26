using System;
using System.Collections.Generic;
using Inventory.Unity;
using Skins;
using UnityEngine;

namespace WizGunBulletAPI
{
    public class Bullet
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public Bullet(string bulletType, string bulletTier, string itemDescription, List<IImpactor> impactorList, string school = "sorcerer", int upgradeLevels = 1)
        {
            this.bulletType = bulletType;
            this.bulletTier = bulletTier;
            this.itemDescription = itemDescription;
            this.school = school;
            this.upgradeLevels = upgradeLevels;
            this.impactorList = impactorList;  
            ConfigureRecipe();
        }

        public void CreateUnityObject()
        {
            gameObject = new GameObject(string.Format("item-bullet-{0}-{1}", bulletType.ToString().ToLower().Replace(" ", "-"), bulletTier));
            item = gameObject.AddComponent<StaticItem>();
            resource = gameObject.AddComponent<StaticItemPrimaryResource>();
            ConfigureItem();
            ConfigureResource();
            recipe.resultItem = gameObject;
        }
        private void ConfigureItem()
        {
            item.arcanaWorth = 10;
            item.canConvertToArcana = true;
            item.displayDescription = new DisplayString
            {
                backupString = itemDescription
            };
            item.displayName = new DisplayString
            {
                backupString = bulletType + " Bullet"
            };
            item.itemRarity = ItemRarity.Common;
            item.icon = icon;
        }
        private void ConfigureResource()
        {
            resource.maxShotCount = 100;
            resource.overrideShotCounts = false;
            resource.stackShotCount = 100;
            resource.pipCost = 1;
            ConfigureSpells(impactorList);
            resource.primarySpells = new GameObject[] {};
        }
        private void ConfigureRecipe()
        {
            recipe = ScriptableObject.CreateInstance<CraftingRecipe>();
            recipe.name = string.Format("recipe-bullet-{0}-{1}", bulletType.ToString().ToLower().Replace(" ", "-"), bulletTier);
            recipe.recipeGroup = 0;
            recipe.requiredArcana = 10;
            recipe.requirements = new RecipeAssetRequirement[0];
        }
        public void ConfigureSpells(List<IImpactor> impactors)
        {
            for (int i = 0; i < upgradeLevels; i++)
            {   
                Spell spell = new Spell(bulletType.ToString().ToLower().Replace(" ", "-"), bulletTier, i.ToString(), impactors);
                WizGunBulletAPI.Log.LogInfo("Spell constructed");
                // spellList.Add(spell);
                WizGunBulletAPI.Log.LogInfo(string.Format("{0} added", bulletType));
            }
        }

        public GameObject gameObject;
        public StaticItem item;
        public StaticItemPrimaryResource resource;
        public CraftingRecipe recipe;
        public string bulletTier;
        public string school;
        public int upgradeLevels;
        public string bulletType;
        public string itemDescription;
        internal Sprite icon;
        public List<Spell> spellList;
        public List<Spell> secondarySpellList;
        public List<IImpactor> impactorList;
    }
}
