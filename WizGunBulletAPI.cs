using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace WizGunBulletAPI
{
    [BepInPlugin("mgr.wwag.bulletapi", "Bullet API", "1.0.0")]
    public class WizGunBulletAPI : BaseUnityPlugin
    {
        public void Awake()
        {
            Log = Logger;
            Log.LogInfo("Bullet API Loaded");
            Harmony harmony = new Harmony("mgr.wwag.bulletapi");
            harmony.Patch(AccessTools.Method(typeof(WorldUtil), "LoadAssetsAsync", null, null), null, new HarmonyMethod(AccessTools.Method(typeof(BulletPatches), "postfix_AssetsLoad", null, null)), null, null, null);
            harmony.Patch(AccessTools.Method(typeof(PlayerSpawnGameData), "CheckUpdateEquipmentList", null, null), new HarmonyMethod(AccessTools.Method(typeof(BulletPatches), "prefix_AddBulletRecipes", null, null)), null, null, null, null);
        }

        public const string pluginGuid = "mgr.wwag.bulletapi";

        public const string pluginName = "Bullet API";

        public const string pluginVersion = "1.0.0";

        internal static ManualLogSource Log;
    }
}
