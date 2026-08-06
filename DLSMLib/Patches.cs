using DLS.Description;
using DLS.Game;
using DLS.Simulation;
using DLSMLib.Chips;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace DLSMLib.Patches
{
    [HarmonyPatch]
    internal static class Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BuiltinChipCreator), "CreateAllBuiltinChipDescriptions")]
        public static void AddToChipDesc(ref Il2CppReferenceArray<ChipDescription> __result)
        {
            var oldLen = __result.Length;

            var newArray = new Il2CppReferenceArray<ChipDescription>(
                oldLen + ChipRegistry.registeredChips.Count
            );

            for (int i = 0; i < oldLen; i++)
                newArray[i] = __result[i];

            int i2 = 0;
            foreach (var kvp in ChipRegistry.registeredChips.Values)
            {
                newArray[oldLen + i2++] = kvp.chipDesc;
            }

            __result = newArray;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(Simulator), "StepChip")]
        public static void StepChip(SimChip chip)
        {
            if (chip == null)
                return;

            if (ChipRegistry.activeChips.TryGetValue(chip, out var mod))
            {
                mod.chipFunction?.Invoke(chip);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Simulator), "BuildSimChipRecursive")]
        static void BuildSimChipRecursive(ChipDescription chipDesc, ref SimChip __result)
        {
            if (ChipRegistry.registeredChips.TryGetValue(chipDesc, out var mod))
            {
                if (__result != null)
                {
                    ChipRegistry.activeChips[__result] = mod;
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChipTypeHelper), "GetName")]
        public static bool GetName(ChipType type, ref string __result)
        {
            if (type == ChipType.Custom)
            {
                return false;
            }

            return true;
        }


        //Mods Menu Patches
        /**[HarmonyPostfix]
        [HarmonyPatch(typeof(MainMenu), "DrawMainScreen")]
        public static void DrawMainScreen()
        {
            int buttonIndex = UI.VerticalButtonGroup(new string[] { "Mods" }, DrawSettings.ActiveUITheme.MainMenuButtonTheme, 
                UI.Centre + Vector2.down * 12.7f, new Vector2(15, 0), false, true, 1);

            if(buttonIndex == 0)
            {
                //Mod Menu
            }
        }*/
    }
}
