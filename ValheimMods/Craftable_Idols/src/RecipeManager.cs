using System;
using BepInEx.Logging;
using UnityEngine;

namespace Craftable_Idols
{
    internal enum CraftingStationType
    {
        Workbench,
        Forge,
        BlackForge
    }

    internal static class RecipeManager
    {
        private static ManualLogSource Log;

        internal static void Initialize(ManualLogSource logger)
        {
            Log = logger;
        }

        internal static void CustomRecipe(
            CraftingStationType stationType,
            string requiredItemA,
            int requiredAmountA,
            string requiredItemB,
            int requiredAmountB,
            string outputItem,
            int outputAmount)
        {
            ObjectDB objectDB = ObjectDB.instance;

            if (objectDB == null)
            {
                Log.LogError(
                    "Cannot add recipe because ObjectDB is null.");

                return;
            }

            // Prevent duplicate recipes.
            string recipeName =
                "Recipe_" + outputItem;

            if (objectDB.m_recipes.Exists(
                recipe => recipe != null &&
                          recipe.name == recipeName))
            {
                Log.LogInfo(
                    $"Recipe already exists: {recipeName}");

                return;
            }

            // Find output item.
            ItemDrop output =
                objectDB.GetItemPrefab(outputItem)
                    ?.GetComponent<ItemDrop>();

            if (output == null)
            {
                Log.LogError(
                    $"Could not find output item: {outputItem}");

                return;
            }

            // Find first ingredient.
            ItemDrop ingredientA =
                objectDB.GetItemPrefab(requiredItemA)
                    ?.GetComponent<ItemDrop>();

            if (ingredientA == null)
            {
                Log.LogError(
                    $"Could not find required item: {requiredItemA}");

                return;
            }

            // Find second ingredient.
            ItemDrop ingredientB =
                objectDB.GetItemPrefab(requiredItemB)
                    ?.GetComponent<ItemDrop>();

            if (ingredientB == null)
            {
                Log.LogError(
                    $"Could not find required item: {requiredItemB}");

                return;
            }

            // Find crafting station.
            CraftingStation craftingStation =
                GetCraftingStation(stationType);

            if (craftingStation == null)
            {
                Log.LogError(
                    $"Could not find crafting station: {stationType}");

                return;
            }

            // Create recipe.
            Recipe recipe =
                ScriptableObject.CreateInstance<Recipe>();

            recipe.name =
                recipeName;

            recipe.m_item =
                output;

            recipe.m_amount =
                outputAmount;

            recipe.m_resources =
                new[]
                {
                    new Piece.Requirement
                    {
                        m_resItem = ingredientA,
                        m_amount = requiredAmountA,
                        m_amountPerLevel = 0,
                        m_recover = true
                    },

                    new Piece.Requirement
                    {
                        m_resItem = ingredientB,
                        m_amount = requiredAmountB,
                        m_amountPerLevel = 0,
                        m_recover = true
                    }
                };

            recipe.m_craftingStation =
                craftingStation;

            objectDB.m_recipes.Add(recipe);

            Log.LogInfo(
                $"Added recipe: {outputItem} x{outputAmount} " +
                $"at {stationType} " +
                $"({requiredItemA} x{requiredAmountA}, " +
                $"{requiredItemB} x{requiredAmountB})");
        }

        private static CraftingStation GetCraftingStation(
            CraftingStationType stationType)
        {
            string prefabName;

            switch (stationType)
            {
                case CraftingStationType.Workbench:
                    prefabName = "piece_workbench";
                    break;

                case CraftingStationType.Forge:
                    prefabName = "forge";
                    break;

                case CraftingStationType.BlackForge:
                    prefabName = "blackforge";
                    break;

                default:
                    return null;
            }

            GameObject prefab =
                ZNetScene.instance.GetPrefab(prefabName);

            if (prefab == null)
            {
                Log.LogError(
                    $"Could not find crafting station prefab: {prefabName}");

                return null;
            }

            CraftingStation station =
                prefab.GetComponent<CraftingStation>();

            if (station == null)
            {
                Log.LogError(
                    $"Prefab '{prefabName}' does not contain a CraftingStation component.");

                return null;
            }

            return station;
        }
    }
}