using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
namespace Craftable_Idols
{
    [BepInPlugin(
        PluginGuid,
        PluginName,
        PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid =
            "com.yuhanma.valheim.craftableidols";

        public const string PluginName =
            "Craftable Idols";

        public const string PluginVersion =
            "1.0.0";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            RecipeManager.Initialize(Log);

            Harmony harmony =
                new Harmony(PluginGuid);

            harmony.PatchAll();

            Log.LogInfo(
                "Craftable Idols loaded.");
        }

        internal static void AddRecipes()
        {
            RecipeManager.CustomRecipe(
                CraftingStationType.Workbench,
                "FineWood",
                20,
                "TrophyEikthyr",
                1,
                "Upgrader0Weapon",
                5);
        }

        [HarmonyPatch(typeof(Player), "OnSpawned")]
        private static class Player_OnSpawned_Patch
        {
            private static void Postfix()
            {
                AddRecipes();
            }
        }
    }
}