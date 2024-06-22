using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UI;

namespace WizGunCosmeticsAPI
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class WizGunCosmeticsAPI : BaseUnityPlugin
    {
        public const string pluginGuid = "cyantist.wwag.cosmeticsapi";
        public const string pluginName = "Cosmetics API";
        public const string pluginVersion = "1.2.0";

        internal static ManualLogSource Log;

        public void Awake()
        {
            Log = Logger;

            Log.LogInfo("Cosmetics API Loaded");

            Harmony harmony = new Harmony(pluginGuid);

            harmony.Patch(AccessTools.Method(typeof(WorldUtil), nameof(WorldUtil.LoadAssetsAsync)), 
                            postfix:new HarmonyMethod(AccessTools.Method(typeof(GlamourPatches), nameof(GlamourPatches.postfix_AssetsLoad))));
            harmony.Patch(AccessTools.Method(typeof(PlayerSpawnGameData), nameof(PlayerSpawnGameData.CheckUpdateEquipmentList)), 
                            prefix: new HarmonyMethod(AccessTools.Method(typeof(GlamourPatches), nameof(GlamourPatches.prefix_AddEquipmentOptions))));
            harmony.Patch(AccessTools.Method(typeof(PlayerSpawnSystem), nameof(PlayerSpawnSystem.GetSystemData)),
                            postfix: new HarmonyMethod(AccessTools.Method(typeof(GlamourPatches), nameof(GlamourPatches.postfix_AddEyeOptions))));
            harmony.Patch(AccessTools.Method(typeof(CustomizeWizardUI), nameof(CustomizeWizardUI.InitializeWindow)),
                            postfix: new HarmonyMethod(AccessTools.Method(typeof(GlamourPatches), nameof(GlamourPatches.postfix_AddEyeOptions))));
        }
    }

    
}
