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
            "com.kiray.valheim.craftableidols";

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
            // Battle Idols
            RecipeManager.CustomRecipe(
                CraftingStationType.Workbench,
                "FineWood", 10,
                "TrophyEikthyr", 1,
                "Upgrader0Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "Bronze", 10,
                "TrophyTheElder", 1,
                "Upgrader1Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "Iron", 10,
                "TrophyBonemass", 1,
                "Upgrader2Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "Silver", 10,
                "TrophyDragonQueen", 1,
                "Upgrader3Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "BlackMetal", 10,
                "TrophyGoblinKing", 1,
                "Upgrader4Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.BlackForge,
                "BlackMarble", 10,
                "TrophySeekerQueen", 1,
                "Upgrader5Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.BlackForge,
                "FlametalNew", 10,
                "TrophyFader", 1,
                "Upgrader6Weapon", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.BlackForge,
                "Gold", 10,
                "FrozenKingDrop", 1,
                "Upgrader7Weapon", 5);

            //Protection Idols
            RecipeManager.CustomRecipe(
                CraftingStationType.Workbench,
                "FineWood", 5,
                "DeerHide", 20,
                "Upgrader0Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "Bronze", 5,
                "TrollHide", 20,
                "Upgrader1Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "Iron", 5,
                "ElderBark", 20,
                "Upgrader2Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "Silver", 5,
                "WolfPelt", 20,
                "Upgrader3Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.Forge,
                "BlackMetal", 5,
                "LoxPelt", 20,
                "Upgrader4Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.BlackForge,
                "BlackMarble", 5,
                "Carapace", 40,
                "Upgrader5Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.BlackForge,
                "FlametalNew", 5,
                "AskHide", 20,
                "Upgrader6Armor", 5);

            RecipeManager.CustomRecipe(
                CraftingStationType.BlackForge,
                "Gold", 5,
                "MooseHide", 20,
                "Upgrader7Armor", 5);

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