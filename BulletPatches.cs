using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGUtil;
using HarmonyLib;
using Skins;
using UnityEngine;
using Unity.Collections;

namespace WizGunBulletAPI
{
    public class BulletPatches
    {
        public static IEnumerator postfix_AssetsLoad(IEnumerator __results)
        {
            __results.MoveNext();
            yield return __results.Current;
            object assetsObj = Traverse.Create(typeof(Assets<GameObject>)).Field("_assets").GetValue();

            if (assetsObj != null)
            {
                if (!AssetsLoadRunning)
                {
                    WizGunBulletAPI.Log.LogInfo("Patching Assets");
                    AssetsLoadRunning = true;
                    BulletManager.CreateUnityObjects();
                    WizGunBulletAPI.Log.LogInfo("Objects created");
                    AddAssets(BulletManager.bulletGameObjects);
                    WizGunBulletAPI.Log.LogInfo("Bullet assets added");
                    SpellManager.CreateUnityObjects();
                    AddAssets((from x in SpellManager.spells select x.gameObject).ToList());
                    WizGunBulletAPI.Log.LogInfo("Spell assets added");
                    foreach (Bullet bullet in BulletManager.bullets)
                    {
                        GameObject spell = GameObject.Find(string.Format("primary-{0}-{1}-{2}", bullet.bulletType.ToLower().Replace(" ", "-"), bullet.bulletTier, "0"));
                        if (spell == null)
                        {
                            spell = GameObject.Find("primary-damage-1");
                        }
                        GameObject bulletObject = GameObject.Find(string.Format("item-bullet-{0}-{1}", bullet.bulletType.ToLower().Replace(" ", "-"), bullet.bulletTier));
                        StaticItemPrimaryResource resource = bulletObject.GetComponent<StaticItemPrimaryResource>();
                        GameObject lightningVisual = GameObject.Find("spell-lightning-5-visuals");
                        if (resource != null)
                        {
                            resource.primarySpells = new GameObject[] { spell };
                        }

                        if (lightningVisual != null)
                        {
                            lightningVisual.transform.parent = spell.transform;
                        }
                        else
                        {
                            WizGunBulletAPI.Log.LogInfo("Could not find lightning visual");
                        }
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        __results.MoveNext();
                        yield return __results.Current;
                    }
                    AddAssets(BulletManager.bulletRecipeBases);
                    for (int i = 0; i < 5; i++)
                    {
                        __results.MoveNext();
                        yield return __results.Current;
                    }
                    List<Assets<RecipeCollection>.AssetEntry> assets = Traverse.Create(typeof(Assets<RecipeCollection>)).Field("_assets").GetValue() as List<Assets<RecipeCollection>.AssetEntry>;
                    foreach(Assets<RecipeCollection>.AssetEntry asset in assets)
                    {
                        if (asset.path == new FixedString128("Assets/Data/Recipes/Crafting/recipe-collection-loadingBench-bullets.asset"))
                        {
                            WizGunBulletAPI.Log.LogInfo("path found");
                            WizGunBulletAPI.Log.LogInfo(BulletManager.bulletRecipes);
                            asset.asset.recipeSubCollections[0].collections[0].collection = asset.asset.recipeSubCollections[0].collections[0].collection.Concat(BulletManager.bulletRecipes).ToArray();
                            Traverse.Create(typeof(Assets<RecipeCollection>)).Field("_assets").SetValue(assets);
                            WizGunBulletAPI.Log.LogInfo("Adjusted RecipeCollection");
                        }
                    }
                    
                    
                    assets = null;
                }
            }
            while (__results.MoveNext())
            {
                yield return __results.Current;
            }
            AssetsLoadRunning = false;
            yield break;
        }

        public static void AddAssets<T>(List<T> newAssets) where T : UnityEngine.Object
        {
            List<Assets<T>.AssetEntry> assets = Traverse.Create(typeof(Assets<T>)).Field("_assets").GetValue() as List<Assets<T>.AssetEntry>;
            foreach (T asset in newAssets)
            {
                Assets<T>.AssetEntry entry = default;
                entry.asset = asset;
                entry.path = string.Format("BulletMod/{0}/{1}", typeof(T).Name, asset.name);
                if (!assets.Contains(entry))
                {
                    assets.Add(entry);

                    WizGunBulletAPI.Log.LogInfo(string.Format("Added {0} to Assets<{1}>", asset.name, typeof(T).Name));
                }
            }
            Traverse.Create(typeof(Assets<T>)).Field("_assets").SetValue(assets);
        }

        // Token: 0x0600000D RID: 13 RVA: 0x000024F8 File Offset: 0x000006F8
        public static void prefix_AddBulletRecipes(RecipeCollection ___equipOptions)
        {
            
            bool flag = ___equipOptions != null && ___equipOptions.recipeSubCollections.Length == 5;
            if (flag)
            {
                WizGunBulletAPI.Log.LogInfo("Patching Bullet Tabs");
                ___equipOptions.recipeSubCollections[0].collections.AddToArray(BulletManager.AddBulletsToRecipeLists());
            }
        }

        // Token: 0x0400000C RID: 12
        public static bool AssetsLoadRunning;
    }
}
