using GGUtil;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizGunCosmeticsAPI
{
    public class GlamourPatches
    {
        public static bool AssetsLoadRunning = false;
        public static IEnumerator postfix_AssetsLoad(IEnumerator __results)
        {
            __results.MoveNext();
            yield return __results.Current;

            object assetsObj = Traverse.Create(typeof(Assets<GameObject>)).Field("_assets").GetValue();
            if (assetsObj != null)
            {
                // Runs twice for some reason?? WIP
                if (!AssetsLoadRunning)
                {
                    WizGunCosmeticsAPI.Log.LogInfo("Patching Assets");
                    AssetsLoadRunning = true;

                    GlamourManager.CreateUnityObjects();

                    AddAssets(GlamourManager.glamourGameObjects);

                    for (int i = 0; i < 9; i++)
                    {
                        __results.MoveNext();
                        yield return __results.Current;
                    }

                    AddAssets(GlamourManager.glamourSkinGroups);

                    __results.MoveNext();
                    yield return __results.Current;

                    AddAssets(GlamourManager.glamourRecipeBases);

                    for (int i = 0; i < 5; i++)
                    {
                        __results.MoveNext();
                        yield return __results.Current;
                    }

                    List<Assets<GlamourRecipeCollection>.AssetEntry> assets = Traverse.Create(typeof(Assets<GlamourRecipeCollection>)).Field("_assets").GetValue() as List<Assets<GlamourRecipeCollection>.AssetEntry>;
                    if (assets.First().asset.glamourCollection.Length == 268)
                    {
                        assets.First().asset.glamourCollection = assets.First().asset.glamourCollection.Concat(GlamourManager.glamourRecipes).ToArray();
                        Traverse.Create(typeof(Assets<GlamourRecipeCollection>)).Field("_assets").SetValue(assets);
                        WizGunCosmeticsAPI.Log.LogInfo("Adjusted GlamourRecipeCollection");
                    }
                }
            }

            while (__results.MoveNext())
            {
                yield return __results.Current;
            }
            AssetsLoadRunning = false;
        }

        public static void AddAssets<T>(List<T> newAssets) where T : UnityEngine.Object
        {
            List<Assets<T>.AssetEntry> assets = Traverse.Create(typeof(Assets<T>)).Field("_assets").GetValue() as List<Assets<T>.AssetEntry>;
            foreach (T asset in newAssets)
            {
                Assets<T>.AssetEntry entry = default;
                entry.asset = asset;
                entry.path = string.Format("CosmeticsMod/{0}/{1}", typeof(T).Name, asset.name);
                if (!assets.Contains(entry))
                {
                    assets.Add(entry);

                    WizGunCosmeticsAPI.Log.LogInfo(string.Format("Added {0} to Assets<{1}>", asset.name, typeof(T).Name));
                }
            }
            Traverse.Create(typeof(Assets<T>)).Field("_assets").SetValue(assets);
        }


        public static void prefix_AddEquipmentOptions(GlamourRecipeCollection ___equipOptions)
        {
            if (___equipOptions != null && ___equipOptions.glamourCollection.Length == 268)
            {
                WizGunCosmeticsAPI.Log.LogInfo("Patching Equipment Options");
                ___equipOptions.glamourCollection = ___equipOptions.glamourCollection.Concat(GlamourManager.glamourRecipes).ToArray();
            }
        }


        public static void postfix_AddEyeOptions(ref PlayerSpawnGameData ____spawnGameData)
        {
            if (____spawnGameData.eyeShapeItems != null && ____spawnGameData.eyeShapeItems.Length == 29)
            {
                WizGunCosmeticsAPI.Log.LogInfo("Patching Eye Options");
                ____spawnGameData.eyeShapeItems = ____spawnGameData.eyeShapeItems.Concat(GlamourManager.eyeGameObjects).ToArray();
            }
        }
    }


}
